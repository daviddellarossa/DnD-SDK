using System;
using System.Collections.Generic;
using System.Linq;
using DeeDeeR.MessageBroker;
using Microsoft.CodeAnalysis; // Core Roslyn APIs
using Microsoft.CodeAnalysis.CSharp; // C#-specific APIs
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerGenerator
    {
        
        internal class CategoryGenerator
        {
            private readonly IEnumerable<MessageInfo> _messageInfos;
            private readonly string _categoryName;
            public CategoryGenerator(string categoryName, IEnumerable<MessageInfo> messageInfos)
            {
                this._categoryName = categoryName;
                this._messageInfos = messageInfos;
            }

            internal void Generate()
            {
                var classes = new List<ClassDeclarationSyntax>();
                
                foreach (var messageInfo in this._messageInfos)
                {
                    // Generate EventArgs-derived class from messageInfo
                    var eventArgsNewClassToAdd = GenerateEventArgsClass(messageInfo);
                    if (eventArgsNewClassToAdd != null)
                    {
                        classes.Add(GenerateEventArgsClass(messageInfo));
                    }
                }

                var categoryClass = GenerateCategoryClass(this._categoryName, this._messageInfos.ToArray());
                
                var usings = GetUsings();
                var @namespace = NamespaceDeclaration(ParseName(_namespace))
                    .AddMembers(classes.Cast<MemberDeclarationSyntax>().ToArray())
                    .AddMembers(categoryClass);
                
                var compilationUnit = CompilationUnit()
                    .AddUsings(usings)
                    .AddMembers(@namespace)
                    .NormalizeWhitespace();
                
                var outputPath = System.IO.Path.Combine(MessageBrokerGenerator._outputFolder, $"{MessageBrokerGenerator.ClassName}.{MessageBrokerGenerator._categoryPrefix}{this._categoryName}.cs");

                this.CreateFile(compilationUnit.ToFullString(), outputPath);
            }

            internal AttributeListSyntax GenerateGeneratedCodeAttribute()
            {
                var generatedCodeAttribute = AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("GeneratedCode"))
                        .AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(nameof(MessageBrokerGenerator)))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(MessageBrokerGenerator._packageVersion)))
                        )
                ));
                return generatedCodeAttribute;
            }

            private SyntaxTriviaList GetSummary(string summary)
            {
                return TriviaList(
                    Trivia(
                        DocumentationCommentTrivia(
                            SyntaxKind.SingleLineDocumentationCommentTrivia,
                            List(
                                new XmlNodeSyntax[]
                                {
                                    XmlText()
                                        .WithTextTokens(
                                            TokenList(
                                                XmlTextLiteral(
                                                    TriviaList(
                                                        DocumentationCommentExterior(
                                                            "///")),
                                                    " ",
                                                    " ",
                                                    TriviaList()))),
                                    XmlExampleElement(
                                            SingletonList<XmlNodeSyntax>(
                                                XmlText()
                                                    .WithTextTokens(
                                                        TokenList(
                                                            new[]
                                                            {
                                                                XmlTextNewLine(
                                                                    TriviaList(),
                                                                    Environment.NewLine,
                                                                    Environment.NewLine,
                                                                    TriviaList()),
                                                                XmlTextLiteral(
                                                                    TriviaList(
                                                                        DocumentationCommentExterior("    ///")),
                                                                    $" {summary}",
                                                                    $" {summary}",
                                                                    TriviaList()),
                                                                XmlTextNewLine(
                                                                    TriviaList(),
                                                                    Environment.NewLine,
                                                                    Environment.NewLine,
                                                                    TriviaList()),
                                                                XmlTextLiteral(
                                                                    TriviaList(
                                                                        SyntaxFactory
                                                                            .DocumentationCommentExterior(
                                                                                "    ///")),
                                                                    " ",
                                                                    " ",
                                                                    TriviaList())
                                                            }))))
                                        .WithStartTag(
                                            XmlElementStartTag(
                                                XmlName(
                                                    Identifier("summary"))))
                                        .WithEndTag(
                                            XmlElementEndTag(
                                                XmlName(
                                                    Identifier("summary")))),
                                    XmlText()
                                        .WithTextTokens(
                                            TokenList(
                                                XmlTextNewLine(
                                                    TriviaList(),
                                                    Environment.NewLine,
                                                    Environment.NewLine,
                                                    TriviaList())))
                                }))));
            }

            private ClassDeclarationSyntax GenerateCategoryClass(string categoryName, MessageInfo[] messageInfos)
            {
                var newClassName = $"{_categoryPrefix}{categoryName}";
                var newClass = ClassDeclaration(newClassName)
                        .WithModifiers(
                            TokenList(
                                Token(
                                    GetSummary(string.Empty),
                                    SyntaxKind.PublicKeyword,
                                    TriviaList())))
                        // Add events
                        .AddMembers(messageInfos.Select(x => EventFieldDeclaration(
                                VariableDeclaration(
                                        GenericName(
                                                Identifier(nameof(EventHandler)))
                                            .WithTypeArgumentList(
                                                TypeArgumentList(
                                                    SingletonSeparatedList<TypeSyntax>(
                                                        IdentifierName(GetEventArgsClassName(x))))))
                                    .WithVariables(
                                        SingletonSeparatedList(
                                            VariableDeclarator(
                                                Identifier(GetEventName(x)))))
                            )
                            .WithModifiers(
                                TokenList(
                                    Token(
                                        GetSummary(x.Message.EventComment),
                                        SyntaxKind.PublicKeyword,
                                        TriviaList())))).Cast<MemberDeclarationSyntax>().ToArray())
                        // Add methods
                        .AddMembers(messageInfos.Select(GenerateSendMethod).Cast<MemberDeclarationSyntax>().ToArray())
                    ;
                
                return newClass;
            }

            private SeparatedSyntaxList<ParameterSyntax> GetParameterList(MessageInfo messageInfo)
            {
                var parameterSyntaxList = new List<SyntaxNodeOrToken>();
                var inputParameters = messageInfo.Message.InputParameters;

                for (int index = 0; index < inputParameters.Count; index++)
                {
                    var parameterSyntax = Parameter(
                            Identifier(inputParameters[index].ParameterName))
                        .WithType(ParseTypeName(GetParameterType(inputParameters[index])));
                    
                    parameterSyntaxList.Add(parameterSyntax);

                    if (index < inputParameters.Count - 1)
                    {
                        parameterSyntaxList.Add(Token(SyntaxKind.CommaToken));
                    }
                }

                return SeparatedList<ParameterSyntax>(parameterSyntaxList);
            }

            private IfStatementSyntax CheckForNull(InputParameter inputParameter)
            {
                var ifStatement = IfStatement(
                    BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName(inputParameter.ParameterName),
                        LiteralExpression(
                            SyntaxKind.NullLiteralExpression)),
                    Block(
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
                                                Identifier("errorEventArgs"))
                                            .WithInitializer(
                                                EqualsValueClause(
                                                    InvocationExpression(
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName(nameof(Common)),
                                                                IdentifierName(
                                                                    nameof(Common.CreateArgumentNullExceptionEventArgs))))
                                                        .WithArgumentList(
                                                            ArgumentList(
                                                                SeparatedList<ArgumentSyntax>(
                                                                    new SyntaxNodeOrToken[]
                                                                    {
                                                                        Argument(
                                                                            LiteralExpression(
                                                                                SyntaxKind.StringLiteralExpression,
                                                                                Literal(
                                                                                    _categoryName))),
                                                                        Token(SyntaxKind.CommaToken),
                                                                        Argument(
                                                                            IdentifierName("target")),
                                                                        Token(SyntaxKind.CommaToken),
                                                                        Argument(
                                                                            LiteralExpression(
                                                                                SyntaxKind.StringLiteralExpression,
                                                                                Literal(inputParameter.ParameterName)))
                                                                    })))))))),
                        ExpressionStatement(
                            InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName("DeeDeeR"),
                                                        IdentifierName("MessageBroker")),
                                                    IdentifierName("MessageBroker")),
                                                IdentifierName("Instance")),
                                            IdentifierName("Logger")),
                                        IdentifierName("Send_OnLogException")))
                                .WithArgumentList(
                                    ArgumentList(
                                        SeparatedList<ArgumentSyntax>(
                                            new SyntaxNodeOrToken[]
                                            {
                                                Argument(
                                                    IdentifierName("sender")),
                                                Token(SyntaxKind.CommaToken),
                                                Argument(
                                                    IdentifierName("target")),
                                                Token(SyntaxKind.CommaToken),
                                                Argument(
                                                    IdentifierName("errorEventArgs"))
                                            })))),
                        ReturnStatement()));
                
                return ifStatement;
            }

            private MethodDeclarationSyntax GenerateSendMethod(MessageInfo messageInfo)
            {
                var eventArgsInnerParameterName = "__eventArgs__";
                return MethodDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.VoidKeyword)),
                        Identifier($"Send_{GetEventName(messageInfo)}"))
                    .WithModifiers(
                        TokenList(
                            Token(
                                GetSummary(messageInfo.Message.SendMethodComment),
                                SyntaxKind.PublicKeyword,
                                TriviaList())))
                    .WithParameterList(
                        ParameterList(GetParameterList(messageInfo)))
                    .WithBody(
                        Block(
                            messageInfo.Message.InputParameters
                                .Where(x =>
                                {
                                    var parameterTypeString = GetParameterType(x);
                                    var parameterType = AppDomain.CurrentDomain.GetAssemblies()
                                        .Select(a => a.GetType(parameterTypeString))
                                        .FirstOrDefault(t => t != null);

                                    if (parameterType is { IsValueType: true })
                                    {
                                        return false;
                                    }

                                    return !x.IsNullable && parameterType is not { IsValueType: true };
                                })
                                .Select(CheckForNull)
                                .Cast<StatementSyntax>()
                                .Concat(new []
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
                                                            Identifier(eventArgsInnerParameterName))
                                                        .WithInitializer(
                                                            EqualsValueClause(
                                                                InvocationExpression(
                                                                    MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            IdentifierName(
                                                                                nameof(MessageBrokerEventArgs)),
                                                                            GenericName(
                                                                                    Identifier("Pool"))
                                                                                .WithTypeArgumentList(
                                                                                    TypeArgumentList(
                                                                                        SyntaxFactory
                                                                                            .SingletonSeparatedList<
                                                                                                TypeSyntax>(
                                                                                                SyntaxFactory
                                                                                                    .IdentifierName(GetEventArgsClassName(messageInfo)))))),
                                                                        IdentifierName("Rent"))))))))

                                })
                                .Concat(
                                    messageInfo.Message.InputParameters
                                        .Select(x =>
                                        {
                                            var expression = ExpressionStatement(
                                                AssignmentExpression(
                                                    SyntaxKind.SimpleAssignmentExpression,
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName(eventArgsInnerParameterName),
                                                        IdentifierName(
                                                            SanitizePropertyName(x.ParameterName))),
                                                    IdentifierName(x.ParameterName)));
                                            return expression;
                                        })
                                )
                                .Concat(new[]
                                    {
                                        SyntaxFactory
                                            .ExpressionStatement(
                                            ConditionalAccessExpression(
                                                IdentifierName(GetEventName(messageInfo)),
                                                InvocationExpression(
                                                        MemberBindingExpression(
                                                            IdentifierName("Invoke")))
                                                    .WithArgumentList(
                                                        ArgumentList(
                                                            SeparatedList<ArgumentSyntax>(
                                                                new SyntaxNodeOrToken[]
                                                                {
                                                                    Argument(
                                                                        IdentifierName("sender")),
                                                                    Token(SyntaxKind.CommaToken),
                                                                    Argument(
                                                                        IdentifierName(eventArgsInnerParameterName))
                                                                }))))
                                                
                                                )
                                    })
                            ));
            }

            private static string GetEventName(MessageInfo x)
            {
                return $"On{x.Message.GetName()}";
            }

            private static string GetEventArgsClassName(MessageInfo x)
            {
                var extraParameters = GetExtraParameters(x);
                if (extraParameters.Any())
                {
                    return $"{ x.Message.GetName() }EventArgs";
                }
                else
                {
                    return nameof(MessageBrokerEventArgs);
                }
            }

            private static InputParameter[] GetExtraParameters(MessageInfo messageInfo)
            {
                var senderParameter = messageInfo.Message.InputParameters.SingleOrDefault(x => x.ParameterName == "sender");
                var targetParameter = messageInfo.Message.InputParameters.SingleOrDefault(x => x.ParameterName == "target");
                var extraInputParameters = messageInfo.Message.InputParameters
                    .Except(new[] { senderParameter, targetParameter }).ToArray();

                return extraInputParameters;
            }

            private ClassDeclarationSyntax GenerateEventArgsClass(MessageInfo messageInfo)
            {
                var extraInputParameters = GetExtraParameters(messageInfo);

                if (extraInputParameters.Any())
                {
                    // Create new EventArgs class
                    var newClassName = GetEventArgsClassName(messageInfo);
                    var newClass = ClassDeclaration(newClassName)
                        .WithModifiers(
                            TokenList(
                                Token(
                                    GetSummary(string.Empty),
                                    SyntaxKind.PublicKeyword,
                                    TriviaList())))
                        .AddBaseListTypes(
                            SimpleBaseType(ParseTypeName(nameof(MessageBrokerEventArgs))), 
                            SimpleBaseType(ParseTypeName(nameof(IResettable))))
                        .AddMembers(extraInputParameters.Select(x =>
                        {
                            var parameterName = SanitizePropertyName(x.ParameterName);
                            var parameterType = GetParameterType(x);
                            TypeSyntax typeSyntax = x.IsNullable
                                ? NullableType(ParseTypeName(parameterType))
                                : ParseTypeName(parameterType);


                            var propertyDeclaration = PropertyDeclaration(typeSyntax, parameterName)
                                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                                .AddAccessorListAccessors(
                                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

                            return propertyDeclaration;
                        }).Cast<MemberDeclarationSyntax>().ToArray())
                        .AddMembers(MethodDeclaration(
                                    PredefinedType(
                                        Token(SyntaxKind.VoidKeyword)),
                                    Identifier(nameof(IResettable.ResetState)))
                                .WithModifiers(TokenList(
                                    Token(
                                        TriviaList(
                                            Trivia(
                                                DocumentationCommentTrivia(
                                            SyntaxKind.SingleLineDocumentationCommentTrivia,
                                            List(
                                                new XmlNodeSyntax[]{
                                                    XmlText()
                                                    .WithTextTokens(
                                                        TokenList(
                                                            XmlTextLiteral(
                                                                TriviaList(
                                                                    DocumentationCommentExterior("///")),
                                                                " ",
                                                                " ",
                                                                TriviaList()))),
                                                    XmlNullKeywordElement()
                                                    .WithName(
                                                        XmlName(
                                                            Identifier("inheritdoc")))
                                                    .WithAttributes(
                                                        SingletonList<XmlAttributeSyntax>(
                                                            XmlCrefAttribute(
                                                                QualifiedCref(
                                                                    IdentifierName(nameof(IResettable)),
                                                                    NameMemberCref(
                                                                        IdentifierName(nameof(IResettable.ResetState))))))),
                                                    XmlText()
                                                    .WithTextTokens(
                                                        TokenList(
                                                            XmlTextNewLine(
                                                                TriviaList(),
                                                                Environment.NewLine,
                                                                Environment.NewLine,
                                                                TriviaList())))})))),
                                SyntaxKind.PublicKeyword,
                                TriviaList())))
                                .WithBody(Block(
                                    extraInputParameters.Select(x => ExpressionStatement(
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                ThisExpression(),
                                                IdentifierName(
                                                    SanitizePropertyName(x.ParameterName))),
                                            LiteralExpression(
                                                SyntaxKind.DefaultLiteralExpression,
                                                Token(SyntaxKind.DefaultKeyword))))).Cast<StatementSyntax>().ToArray()))
                        );
                    return newClass;
                }

                return null;
            }

            private string SanitizePropertyName(string propertyName)
            {
                var cleanPropertyName = MessageBrokerGenerator.CleanName(propertyName);
                return char.ToUpper(cleanPropertyName[0]) + cleanPropertyName.Substring(1);
            }

            private static string GetParameterType(InputParameter inputParameter)
            {
                var multiplicity = inputParameter.Multiplicity.ToTypeString();
                
                return inputParameter.ParameterType switch
                {
                    ParameterType.BooleanType => string.Format(multiplicity, Token(SyntaxKind.BoolKeyword).ValueText),
                    ParameterType.ByteType => string.Format(multiplicity, Token(SyntaxKind.ByteKeyword).ValueText),
                    ParameterType.DoubleType => string.Format(multiplicity, Token(SyntaxKind.DoubleKeyword).ValueText),
                    ParameterType.FloatType => string.Format(multiplicity, Token(SyntaxKind.FloatKeyword).ValueText),
                    ParameterType.IntType => string.Format(multiplicity, Token(SyntaxKind.IntKeyword).ValueText),
                    ParameterType.LongType => string.Format(multiplicity, Token(SyntaxKind.LongKeyword).ValueText),
                    ParameterType.ShortType => string.Format(multiplicity, Token(SyntaxKind.ShortKeyword).ValueText),
                    ParameterType.StringType => string.Format(multiplicity, Token(SyntaxKind.StringKeyword).ValueText),
                    ParameterType.VoidType => Token(SyntaxKind.VoidKeyword).ValueText,
                    ParameterType.TransformType => string.Format(multiplicity, QualifiedName(IdentifierName("UnityEngine"), IdentifierName("Transform")).ToFullString()),
                    ParameterType.ObjectType => string.Format(multiplicity, Token(SyntaxKind.ObjectKeyword).ValueText),
                    ParameterType.OtherType => string.Format(multiplicity, inputParameter.OtherType),
                    _ => throw new NotImplementedException(),
                };
            }
            
            public static string ToTypeString(Multiplicity multiplicity)
            {
                return multiplicity switch
                {
                    Multiplicity.Single => "{0}",
                    Multiplicity.Array => "{0}[]",
                    Multiplicity.Collection => "Collection<{0}>",
                    Multiplicity.ICollection => "ICollection<{0}>",
                    Multiplicity.IList => "IList<{0}>",
                    Multiplicity.List => "List<{0}>",
                    Multiplicity.IEnumerable => "IEnumerable<{0}>",
                    _ => throw new NotSupportedException("Type not supported")
                };
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
                    UsingDirective(ParseName("UnityEngine")),
                    UsingDirective(ParseName("UnityEditor")),
                    UsingDirective(ParseName("MessageBroker")),
                    UsingDirective(ParseName("System.Collections.Generic")),
                };
            }
            
            private static SyntaxTriviaList GetHeader()
            {
                return TriviaList(
                    Comment("//------------------------------------------------------------------------------"),
                    Comment("// <auto-generated>"),
                    Comment($"// Code auto-generated by {nameof(CategoryGenerator)} version {MessageBrokerGenerator._packageVersion}."),
                    Comment($"// Re-run the generator every time a new {nameof(Message)} is added or removed."),
                    Comment("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated."),
                    Comment("// </auto-generated>"),
                    Comment("//------------------------------------------------------------------------------"),
                    ElasticCarriageReturnLineFeed // add a blank line
                );
            }
            
            private void CreateFile(string fileContent, string outputPath)
            {
                if (!System.IO.Directory.Exists(_outputFolder))
                {
                    System.IO.Directory.CreateDirectory(_outputFolder);
                }

                System.IO.File.WriteAllText(outputPath, fileContent);
            }
        }
    }
}