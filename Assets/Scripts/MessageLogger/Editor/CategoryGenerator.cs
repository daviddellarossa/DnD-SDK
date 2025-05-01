using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis; // Core Roslyn APIs
using Microsoft.CodeAnalysis.CSharp; // C#-specific APIs
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UnityEditor;
using UnityEngine;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MessageLogger.Editor
{
    public class CategoryGenerator
    {
        private readonly IEnumerable<EventInfo> _eventInfos;
        private readonly string _categoryName;
        private static string PackageVersion { get; set; } = "0.0.0";
        private static readonly string SubscribeMethodName = "Subscribe";
        private static readonly string UnsubscribeMethodName = "Unsubscribe";
        private readonly string _messageBrokerTypeName;

        private readonly string _namespace;
        private readonly string _outputFolder;

        public CategoryGenerator(string categoryName, string @namespace, string outputFolder, EventInfo[] events, string messageBrokerTypeName)
        {
            this._categoryName = categoryName;
            this._eventInfos = events;
            this._namespace = @namespace;
            this._outputFolder = outputFolder;
            this._messageBrokerTypeName = messageBrokerTypeName;
        }

        public void Generate()
        {
            Debug.Log("MessageLoggerGenerator started.");
            var categoryClass = GenerateCategoryClass(this._categoryName, this._eventInfos.ToArray());
                
            var usings = GetUsings();
            
            var @namespace = NamespaceDeclaration(ParseName(_namespace))
                .AddMembers(categoryClass);
                
            var compilationUnit = CompilationUnit()
                .AddUsings(usings)
                .AddMembers(@namespace)
                .NormalizeWhitespace();
                
            var outputPath = System.IO.Path.Combine(_outputFolder, $"{this._categoryName}Category.cs");

            var content = compilationUnit.ToFullString();
            this.CreateFile(content, outputPath);
        }

        private void CreateFile(string fileContent, string outputPath)
        {                
            if (!System.IO.Directory.Exists(_outputFolder))
            {
                System.IO.Directory.CreateDirectory(_outputFolder);
            }

            System.IO.File.WriteAllText(outputPath, fileContent);
        }

        private UsingDirectiveSyntax[] GetUsings()
        {
            return new[]
            {
                UsingDirective(ParseName("System"))
                    .WithUsingKeyword(
                        Token(
                            GetHeader(),
                            SyntaxKind.UsingKeyword,
                            TriviaList())),
                UsingDirective(ParseName("System.Collections.Generic")),
            };
        }

        private ClassDeclarationSyntax GenerateCategoryClass(string categoryName, object toArray)
        {
            var newClassName = $"{categoryName}Category";
            var newClass = ClassDeclaration(newClassName)
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.InternalKeyword)))
                .WithBaseList(
                    BaseList(
                        SingletonSeparatedList<BaseTypeSyntax>(
                            SimpleBaseType(
                                IdentifierName(nameof(MessageCategory))))))
                .AddMembers(GenerateSubscribeMethod())
                .AddMembers(GenerateUnsubscribeMethod())
                .AddMembers(GenerateEventHandlers());

            return newClass;
        }

        private MemberDeclarationSyntax[] GenerateEventHandlers()
        {
            var methods = new List<MemberDeclarationSyntax>();
            foreach (var eventInfo in _eventInfos)
            {
                // Define a class to store parameter information
                var parameterInfos = ExtractEventParameters(eventInfo);
                
                // Create parameter list for method signature
                var parameterSyntaxList = new List<SyntaxNodeOrToken>();
                
                for (int i = 0; i < parameterInfos.Count; i++)
                {
                    var param = parameterInfos[i];
                    
                    parameterSyntaxList.Add(
                        Parameter(Identifier(param.Name))
                            .WithType(param.TypeSyntax)
                    );
                    
                    // Add comma if not the last parameter
                    if (i < parameterInfos.Count - 1)
                    {
                        parameterSyntaxList.Add(Token(SyntaxKind.CommaToken));
                    }
                }
                
                var parameterList = ParameterList(SeparatedList<ParameterSyntax>(parameterSyntaxList));
                
                var method = MethodDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.VoidKeyword)),
                        Identifier($"Handle_{eventInfo.Name}"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PrivateKeyword)))
                    .WithParameterList(parameterList)
                    .WithBody(
                        Block(
                            SingletonList<StatementSyntax>(
                                ExpressionStatement(
                                    InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("Logger"),
                                                IdentifierName("LogEvent")))
                                        .WithArgumentList(ArgumentList(
                                            SingletonSeparatedList(
                                                Argument(
                                                    IdentifierName("e")))))))));
                methods.Add(method);
            }
            return methods.ToArray();
        }

        private MemberDeclarationSyntax GenerateUnsubscribeMethod()
        {
            return MethodDeclaration(
                    PredefinedType(
                        Token(SyntaxKind.VoidKeyword)),
                    Identifier(UnsubscribeMethodName))
                .WithModifiers(
                    TokenList(
                        new[]
                        {
                            Token(SyntaxKind.ProtectedKeyword),
                            Token(SyntaxKind.OverrideKeyword)
                        }))
                .WithBody(
                    Block(
                        new StatementSyntax[]
                        {
                            LocalDeclarationStatement(
                                VariableDeclaration(
                                        IdentifierName(
                                            Identifier(
                                                TriviaList(),
                                                SyntaxKind.VarKeyword,
                                                "var",
                                                "var",
                                                TriviaList())))
                                    .WithVariables(
                                        SingletonSeparatedList(
                                            VariableDeclarator(
                                                    Identifier("instance"))
                                                .WithInitializer(
                                                    EqualsValueClause(
                                                        ParseExpression(_messageBrokerTypeName))))))
                        }.Concat(
                            _eventInfos.Select(eventInfo =>
                                ExpressionStatement(
                                    AssignmentExpression(
                                        SyntaxKind.SubtractAssignmentExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("instance"),
                                            IdentifierName(eventInfo.Name)),
                                        IdentifierName($"Handle_{eventInfo.Name}")))))));

        }

        private MemberDeclarationSyntax GenerateSubscribeMethod()
        {
            return MethodDeclaration(
                    PredefinedType(
                        Token(SyntaxKind.VoidKeyword)),
                    Identifier(SubscribeMethodName))
                .WithModifiers(
                    TokenList(
                        new[]
                        {
                            Token(SyntaxKind.ProtectedKeyword),
                            Token(SyntaxKind.OverrideKeyword)
                        }))
                .WithBody(
                    Block(
                        new StatementSyntax[]
                        {
                            LocalDeclarationStatement(
                                VariableDeclaration(
                                        IdentifierName(
                                            Identifier(
                                                TriviaList(),
                                                SyntaxKind.VarKeyword,
                                                "var",
                                                "var",
                                                TriviaList())))
                                    .WithVariables(
                                        SingletonSeparatedList(
                                            VariableDeclarator(
                                                    Identifier("instance"))
                                                .WithInitializer(
                                                    EqualsValueClause(
                                                        ParseExpression(_messageBrokerTypeName))))))
                        }.Concat(
                            _eventInfos.Select(eventInfo =>
                                ExpressionStatement(
                                    AssignmentExpression(
                                        SyntaxKind.AddAssignmentExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("instance"),
                                            IdentifierName(eventInfo.Name)),
                                        IdentifierName($"Handle_{eventInfo.Name}")))))));
        }

        private static SyntaxTriviaList GetHeader()
        {
            return TriviaList(
                Comment("//------------------------------------------------------------------------------"),
                Comment("// <auto-generated>"),
                Comment($"// Code auto-generated by {nameof(CategoryGenerator)} version {PackageVersion}."),
                Comment($"// Re-run the generator every time the Logger needs to be updated."),
                Comment(
                    "// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated."),
                Comment("// </auto-generated>"),
                Comment("//------------------------------------------------------------------------------"),
                ElasticCarriageReturnLineFeed // add a blank line
            );
        }

        /// <summary>
        /// Class to hold parameter information extracted from event handlers
        /// </summary>
        private class ParameterInfo
        {
            public string Name { get; set; }
            public System.Type Type { get; set; }
            public TypeSyntax TypeSyntax { get; set; }
        }
        
        /// <summary>
        /// Extracts parameter information from an EventInfo object
        /// </summary>
        private List<ParameterInfo> ExtractEventParameters(EventInfo eventInfo)
        {
            var result = new List<ParameterInfo>();
            
            // Get the Invoke method from the event handler delegate type
            var invokeMethod = eventInfo.EventHandlerType.GetMethod("Invoke");
            if (invokeMethod == null)
            {
                return result;
            }
            
            // Get parameters from the Invoke method
            var parameters = invokeMethod.GetParameters();
            
            foreach (var parameter in parameters)
            {
                var paramName = parameter.Name;
                var paramType = parameter.ParameterType;
                TypeSyntax typeSyntax;
                
                // Handle different type scenarios
                if (paramType.IsGenericType)
                {
                    // Handle generic types (like EventHandler<T>)
                    string genericTypeName = paramType.GetGenericTypeDefinition().Name;
                    genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                    
                    var genericArgs = paramType.GetGenericArguments();
                    var typeArgList = new List<TypeSyntax>();
                    
                    foreach (var genericArg in genericArgs)
                    {
                        // Process nested generic types recursively if needed
                        if (genericArg.IsGenericType)
                        {
                            // For complex nested generics - simplified handling here
                            typeArgList.Add(ParseTypeName(genericArg.Name));
                        }
                        else
                        {
                            // For simple types
                            typeArgList.Add(ParseTypeName(genericArg.FullName ?? genericArg.Name));
                        }
                    }
                    
                    // Create generic name syntax
                    typeSyntax = GenericName(Identifier(genericTypeName))
                        .WithTypeArgumentList(TypeArgumentList(SeparatedList(typeArgList)));
                }
                else if (paramType.IsArray)
                {
                    // Handle array types
                    var elementType = paramType.GetElementType()!;
                    typeSyntax = ArrayType(
                        ParseTypeName(elementType.FullName ?? elementType.Name),
                        SingletonList(ArrayRankSpecifier())
                    );
                }
                else
                {
                    // Handle regular types
                    // Try to use full name for better resolution
                    string typeName = paramType.FullName ?? paramType.Name;
                    
                    // Special handling for common types to use keywords
                    typeName = typeName switch
                    {
                        "System.String" => "string",
                        "System.Int32" => "int",
                        "System.Boolean" => "bool",
                        "System.Void" => "void",
                        "System.Object" => "object",
                        "System.Double" => "double",
                        "System.Single" => "float",
                        _ => typeName
                    };
                    
                    typeSyntax = ParseTypeName(typeName);
                }
                
                result.Add(new ParameterInfo
                {
                    Name = paramName,
                    Type = paramType,
                    TypeSyntax = typeSyntax
                });
            }
            
            return result;
        }
    }
}
