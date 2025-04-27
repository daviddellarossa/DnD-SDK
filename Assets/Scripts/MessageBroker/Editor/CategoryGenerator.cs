using System;
using System.Collections.Generic;
using System.Linq;
using DeeDeeR.MessageBroker;
using Microsoft.CodeAnalysis; // Core Roslyn APIs
using Microsoft.CodeAnalysis.CSharp; // C#-specific APIs
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine; // For working with syntax nodes
//using Microsoft.CodeAnalysis.Formatting; // Formatting utilities (optional)

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

                // TODO: Generate the Category class here
                
                var categoryClass = GenerateCategoryClass(this._categoryName, this._messageInfos.ToArray());
                
                var usings = GetUsings();
                var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(MessageBrokerGenerator._namespace))
                    .AddMembers(classes.Cast<MemberDeclarationSyntax>().ToArray())
                    .AddMembers(categoryClass);
                
                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddUsings(usings)
                    .AddMembers(@namespace)
                    .NormalizeWhitespace();

                var str = compilationUnit.ToFullString();
            }

            internal ClassDeclarationSyntax GenerateCategoryClass(string categoryName, MessageInfo[] messageInfos)
            {
                var newClassName =
                    $"{Indent.Get()} public class {MessageBrokerGenerator._categoryPrefix}{categoryName}";
                var newClass = SyntaxFactory.ClassDeclaration(newClassName)
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                        // Add events
                        .AddMembers(messageInfos.Select(x =>
                        {
                            return SyntaxFactory.EventFieldDeclaration(
                                    SyntaxFactory.VariableDeclaration(
                                            SyntaxFactory.GenericName(
                                                    SyntaxFactory.Identifier(nameof(EventHandler)))
                                                .WithTypeArgumentList(
                                                    SyntaxFactory.TypeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                            SyntaxFactory.IdentifierName(GetEventArgsClassName(x))))))
                                        .WithVariables(
                                            SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(GetEventName(x)))))
                                )
                                .WithModifiers(
                                    SyntaxFactory.TokenList(
                                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
                        }).Cast<MemberDeclarationSyntax>().ToArray())
                        // Add methods
                        .AddMembers(messageInfos.Select(GenerateSendMethod).Cast<MemberDeclarationSyntax>().ToArray())
                    ;
                
                return newClass;
            }

            internal SeparatedSyntaxList<ParameterSyntax> GetParameterList(MessageInfo messageInfo)
            {
                var parameterSyntaxList = new List<SyntaxNodeOrToken>();
                var inputParameters = messageInfo.Message.InputParameters;

                for (int index = 0; index < inputParameters.Count; index++)
                {
                    var parameterSyntax = SyntaxFactory.Parameter(
                            SyntaxFactory.Identifier(inputParameters[index].ParameterName))
                        .WithType(SyntaxFactory.ParseTypeName(GetParameterType(inputParameters[index])));
                    
                    parameterSyntaxList.Add(parameterSyntax);

                    if (index < inputParameters.Count - 1)
                    {
                        parameterSyntaxList.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                    }
                }

                return SyntaxFactory.SeparatedList<ParameterSyntax>(parameterSyntaxList);
            }

            internal IfStatementSyntax CheckForNull(InputParameter inputParameter)
            {
                var ifStatement = SyntaxFactory.IfStatement(
                    SyntaxFactory.BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        SyntaxFactory.IdentifierName(inputParameter.ParameterName),
                        SyntaxFactory.LiteralExpression(
                            SyntaxKind.NullLiteralExpression)),
                    SyntaxFactory.Block(
                        SyntaxFactory.LocalDeclarationStatement(
                            SyntaxFactory.VariableDeclaration(
                                    SyntaxFactory.IdentifierName(
                                        SyntaxFactory.Identifier(
                                            SyntaxFactory.TriviaList(),
                                            SyntaxKind.VarKeyword,
                                            "var",
                                            "var",
                                            SyntaxFactory.TriviaList())))
                                .WithVariables(
                                    SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                        SyntaxFactory.VariableDeclarator(
                                                SyntaxFactory.Identifier("errorEventArgs"))
                                            .WithInitializer(
                                                SyntaxFactory.EqualsValueClause(
                                                    SyntaxFactory.InvocationExpression(
                                                            SyntaxFactory.MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                SyntaxFactory.IdentifierName(nameof(Common)),
                                                                SyntaxFactory.IdentifierName(
                                                                    nameof(Common.CreateArgumentNullExceptionEventArgs))))
                                                        .WithArgumentList(
                                                            SyntaxFactory.ArgumentList(
                                                                SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                                                    new SyntaxNodeOrToken[]
                                                                    {
                                                                        SyntaxFactory.Argument(
                                                                            SyntaxFactory.LiteralExpression(
                                                                                SyntaxKind.StringLiteralExpression,
                                                                                SyntaxFactory.Literal(
                                                                                    _categoryName))),
                                                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                                        SyntaxFactory.Argument(
                                                                            SyntaxFactory.IdentifierName("target")),
                                                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                                        SyntaxFactory.Argument(
                                                                            SyntaxFactory.LiteralExpression(
                                                                                SyntaxKind.StringLiteralExpression,
                                                                                SyntaxFactory.Literal(inputParameter.ParameterName)))
                                                                    })))))))),
                        SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.InvocationExpression(
                                    SyntaxFactory.MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        SyntaxFactory.IdentifierName("DeeDeeR"),
                                                        SyntaxFactory.IdentifierName("MessageBroker")),
                                                    SyntaxFactory.IdentifierName("MessageBroker")),
                                                SyntaxFactory.IdentifierName("Instance")),
                                            SyntaxFactory.IdentifierName("Logger_Test")),
                                        SyntaxFactory.IdentifierName("Send_OnLogException")))
                                .WithArgumentList(
                                    SyntaxFactory.ArgumentList(
                                        SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                            new SyntaxNodeOrToken[]
                                            {
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.IdentifierName("sender")),
                                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.IdentifierName("target")),
                                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.IdentifierName("errorEventArgs"))
                                            })))),
                        SyntaxFactory.ReturnStatement()));
                
                return ifStatement;
            }
            
            internal MethodDeclarationSyntax GenerateSendMethod(MessageInfo messageInfo)
            {
                return SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.PredefinedType(
                            SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                        SyntaxFactory.Identifier($"Send_{GetEventName(messageInfo)}"))
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        SyntaxFactory.ParameterList(GetParameterList(messageInfo)))
                    .WithBody(
                        SyntaxFactory.Block(
                            messageInfo.Message.InputParameters
                                .Where(x => !x.IsNullable)
                                .Select(x =>
                                {
                                    return CheckForNull(x);
                                })
                                .Cast<StatementSyntax>()
                                .Concat(new []
                                {
                                    SyntaxFactory.LocalDeclarationStatement(
                                        SyntaxFactory.VariableDeclaration(
                                                SyntaxFactory.IdentifierName(
                                                    SyntaxFactory.Identifier(
                                                        SyntaxFactory.TriviaList(),
                                                        SyntaxKind.VarKeyword,
                                                        "var",
                                                        "var",
                                                        SyntaxFactory.TriviaList())))
                                            .WithVariables(
                                                SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                    SyntaxFactory.VariableDeclarator(
                                                            SyntaxFactory.Identifier("eventArgs"))
                                                        .WithInitializer(
                                                            SyntaxFactory.EqualsValueClause(
                                                                SyntaxFactory.InvocationExpression(
                                                                    SyntaxFactory.MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        SyntaxFactory.MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            SyntaxFactory.IdentifierName(
                                                                                nameof(MessageBrokerEventArgs)),
                                                                            SyntaxFactory.GenericName(
                                                                                    SyntaxFactory.Identifier("Pool"))
                                                                                .WithTypeArgumentList(
                                                                                    SyntaxFactory.TypeArgumentList(
                                                                                        SyntaxFactory
                                                                                            .SingletonSeparatedList<
                                                                                                TypeSyntax>(
                                                                                                SyntaxFactory
                                                                                                    .IdentifierName(GetEventArgsClassName(messageInfo)))))),
                                                                        SyntaxFactory.IdentifierName("Rent"))))))))

                                })
                                .Concat(
                                    messageInfo.Message.InputParameters
                                        .Select(x =>
                                        {
                                            var expression = SyntaxFactory.ExpressionStatement(
                                                SyntaxFactory.AssignmentExpression(
                                                    SyntaxKind.SimpleAssignmentExpression,
                                                    SyntaxFactory.MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        SyntaxFactory.IdentifierName("eventArgs"),
                                                        SyntaxFactory.IdentifierName(
                                                            SanitizePropertyName(x.ParameterName))),
                                                    SyntaxFactory.IdentifierName(x.ParameterName)));
                                            return expression;
                                        })
                                )
                                .Concat(new[]
                                    {
                                        SyntaxFactory
                                            .ExpressionStatement(
                                            SyntaxFactory.ConditionalAccessExpression(
                                                SyntaxFactory.IdentifierName(GetEventName(messageInfo)),
                                                SyntaxFactory.InvocationExpression(
                                                        SyntaxFactory.MemberBindingExpression(
                                                            SyntaxFactory.IdentifierName("Invoke")))
                                                    .WithArgumentList(
                                                        SyntaxFactory.ArgumentList(
                                                            SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                                                new SyntaxNodeOrToken[]
                                                                {
                                                                    SyntaxFactory.Argument(
                                                                        SyntaxFactory.IdentifierName("sender")),
                                                                    SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                                    SyntaxFactory.Argument(
                                                                        SyntaxFactory.IdentifierName("eventArgs"))
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

            public static InputParameter[] GetExtraParameters(MessageInfo messageInfo)
            {
                var senderParameter = messageInfo.Message.InputParameters.SingleOrDefault(x => x.ParameterName == "sender");
                var targetParameter = messageInfo.Message.InputParameters.SingleOrDefault(x => x.ParameterName == "target");
                var extraInputParameters = messageInfo.Message.InputParameters
                    .Except(new[] { senderParameter, targetParameter }).ToArray();

                return extraInputParameters;
            }

            internal ClassDeclarationSyntax GenerateEventArgsClass(MessageInfo messageInfo)
            {
                var extraInputParameters = GetExtraParameters(messageInfo);

                if (extraInputParameters.Any())
                {
                    // Create new EventArgs class
                    var newClassName = GetEventArgsClassName(messageInfo);
                    var newClass = SyntaxFactory.ClassDeclaration(newClassName)
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                        .AddBaseListTypes(new[]
                        {
                            SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(nameof(MessageBrokerEventArgs))),
                            SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(nameof(IResettable)))
                        })
                        .AddMembers(extraInputParameters.Select(x =>
                        {
                            var parameterName = SanitizePropertyName(x.ParameterName);
                            var parameterType = GetParameterType(x);
                            TypeSyntax typeSyntax = x.IsNullable
                                ? SyntaxFactory.NullableType(SyntaxFactory.ParseTypeName(parameterType))
                                : SyntaxFactory.ParseTypeName(parameterType);


                            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(typeSyntax, parameterName)
                                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

                            return propertyDeclaration;
                        }).Cast<MemberDeclarationSyntax>().ToArray())
                        .AddMembers(SyntaxFactory.MethodDeclaration(
                                    SyntaxFactory.PredefinedType(
                                        SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                                    SyntaxFactory.Identifier(nameof(IResettable.ResetState)))
                                .WithModifiers(SyntaxFactory.TokenList(
                                    SyntaxFactory.Token(
                                        SyntaxFactory.TriviaList(
                                            SyntaxFactory.Trivia(
                                                SyntaxFactory.DocumentationCommentTrivia(
                                            SyntaxKind.SingleLineDocumentationCommentTrivia,
                                            SyntaxFactory.List<XmlNodeSyntax>(
                                                new XmlNodeSyntax[]{
                                                    SyntaxFactory.XmlText()
                                                    .WithTextTokens(
                                                        SyntaxFactory.TokenList(
                                                            SyntaxFactory.XmlTextLiteral(
                                                                SyntaxFactory.TriviaList(
                                                                    SyntaxFactory.DocumentationCommentExterior("///")),
                                                                " ",
                                                                " ",
                                                                SyntaxFactory.TriviaList()))),
                                                    SyntaxFactory.XmlNullKeywordElement()
                                                    .WithName(
                                                        SyntaxFactory.XmlName(
                                                            SyntaxFactory.Identifier("inheritdoc")))
                                                    .WithAttributes(
                                                        SyntaxFactory.SingletonList<XmlAttributeSyntax>(
                                                            SyntaxFactory.XmlCrefAttribute(
                                                                SyntaxFactory.QualifiedCref(
                                                                    SyntaxFactory.IdentifierName(nameof(IResettable)),
                                                                    SyntaxFactory.NameMemberCref(
                                                                        SyntaxFactory.IdentifierName(nameof(IResettable.ResetState))))))),
                                                    SyntaxFactory.XmlText()
                                                    .WithTextTokens(
                                                        SyntaxFactory.TokenList(
                                                            SyntaxFactory.XmlTextNewLine(
                                                                SyntaxFactory.TriviaList(),
                                                                Environment.NewLine,
                                                                Environment.NewLine,
                                                                SyntaxFactory.TriviaList())))})))),
                                SyntaxKind.PublicKeyword,
                                SyntaxFactory.TriviaList())))
                                .WithBody(SyntaxFactory.Block(
                                    extraInputParameters.Select(x => SyntaxFactory.ExpressionStatement(
                                        SyntaxFactory.AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.ThisExpression(),
                                                SyntaxFactory.IdentifierName(
                                                    SanitizePropertyName(x.ParameterName))),
                                            SyntaxFactory.LiteralExpression(
                                                SyntaxKind.DefaultLiteralExpression,
                                                SyntaxFactory.Token(SyntaxKind.DefaultKeyword))))).Cast<StatementSyntax>().ToArray()))
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
            
            internal static string GetParameterType(InputParameter inputParameter)
            {
                return inputParameter.ParameterType switch
                {
                    ParameterType.BooleanType => SyntaxFactory.Token(SyntaxKind.BoolKeyword).ValueText,
                    ParameterType.ByteType => SyntaxFactory.Token(SyntaxKind.ByteKeyword).ValueText,
                    ParameterType.DoubleType => SyntaxFactory.Token(SyntaxKind.DoubleKeyword).ValueText,
                    ParameterType.FloatType => SyntaxFactory.Token(SyntaxKind.FloatKeyword).ValueText,
                    ParameterType.IntType => SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText,
                    ParameterType.LongType => SyntaxFactory.Token(SyntaxKind.LongKeyword).ValueText,
                    ParameterType.ShortType => SyntaxFactory.Token(SyntaxKind.ShortKeyword).ValueText,
                    ParameterType.StringType => SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText,
                    ParameterType.VoidType => SyntaxFactory.Token(SyntaxKind.VoidKeyword).ValueText,
                    ParameterType.TransformType => SyntaxFactory.QualifiedName(SyntaxFactory.IdentifierName("UnityEngine"), SyntaxFactory.IdentifierName("Transform")).ToFullString(),
                    ParameterType.ObjectType => SyntaxFactory.Token(SyntaxKind.ObjectKeyword).ValueText,
                    ParameterType.OtherType => inputParameter.OtherType,
                    _ => throw new NotImplementedException(),
                };
                var parameterType = inputParameter.ParameterType.ToParameterTypeString(System.Data.ParameterDirection.Input, false, inputParameter.OtherType);

                var multiplicity = inputParameter.Multiplicity.ToTypeString();

                return string.Format(multiplicity, parameterType);
            }

            private void GenerateFilePerCategory(IGrouping<string, MessageInfo> messageGroup)
            {
                var messageInfoGroup = messageGroup.ToArray();

                var categoryName = MessageBrokerGenerator.CleanName(string.IsNullOrWhiteSpace(messageGroup.Key) ? MessageBrokerGenerator._defaultCategoryName : messageGroup.Key);

                var headerTrivia = GetHeader();

                var usings = GetUsings();
                
                var eventMembers = new List<MemberDeclarationSyntax>();
                
                var sendMethodsMembers = new List<MemberDeclarationSyntax>();

                
                foreach (var messageInfo in messageInfoGroup)
                {
                    // create <messageInfoName>EventArgs
                    //eventMembers.Add(GenerateEventHandlerEvent(messageInfo.Message.GetName()));
                    
                    sendMethodsMembers.Add(GenerateSendCharacterCreatedMethod(messageInfo.Message.GetName()));
                }
                
                var @class = SyntaxFactory.ClassDeclaration($"{MessageBrokerGenerator._categoryPrefix}{categoryName}" + "_Test")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                
                eventMembers.ForEach(x => @class = @class.AddMembers(x));
                
                // Add a blank line as trivia
                var blankLineTrivia = SyntaxFactory.TriviaList(
                    SyntaxFactory.ElasticCarriageReturnLineFeed, // Adds a blank line
                    SyntaxFactory.ElasticCarriageReturnLineFeed
                );


                sendMethodsMembers.ForEach(x => @class = @class.AddMembers(x));

                    //.AddMembers(eventField, method);
                
                var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(MessageBrokerGenerator._namespace))
                    .AddMembers(@class);
                
                // Attach the header trivia to the CompilationUnit
                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddUsings(usings)
                    .AddMembers(@namespace)
                    .WithLeadingTrivia(headerTrivia)
                    .NormalizeWhitespace();
                
                var outputPath = System.IO.Path.Combine(MessageBrokerGenerator._outputFolder, $"{MessageBrokerGenerator.ClassName}_Test.{MessageBrokerGenerator._categoryPrefix}{categoryName}.cs");

                this.CreateFile(compilationUnit.ToFullString(), outputPath);
            }

            private UsingDirectiveSyntax[] GetUsings()
            {
                return new[]
                {
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System"))
                        .WithUsingKeyword(
                            SyntaxFactory.Token(
                                GetFileHeader(),
                                SyntaxKind.UsingKeyword,
                                SyntaxFactory.TriviaList())),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("UnityEngine")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("UnityEditor")),
                };
            }

            private SyntaxTriviaList GetFileHeader()
            {
                return
                    SyntaxFactory.TriviaList(
                        new[]
                        {
                            SyntaxFactory.Comment("//------------------------------------------------------------------------------"),
                            SyntaxFactory.Comment("// <auto-generated>"),
                            SyntaxFactory.Comment("// Code auto-generated by CategoryGenerator version <version undefined>."),
                            SyntaxFactory.Comment("// Re-run the generator every time a new Message is added or removed."),
                            SyntaxFactory.Comment("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated."),
                            SyntaxFactory.Comment("// </auto-generated>"),
                            SyntaxFactory.Comment("//------------------------------------------------------------------------------")
                        });
            }

            private static SyntaxTriviaList GetHeader()
            {
                return SyntaxFactory.TriviaList(
                    SyntaxFactory.Comment("//------------------------------------------------------------------------------"),
                    SyntaxFactory.Comment("// <auto-generated>"),
                    SyntaxFactory.Comment($"// Code auto-generated by {nameof(CategoryGenerator)} version {MessageBrokerGenerator._packageVersion}."),
                    SyntaxFactory.Comment($"// Re-run the generator every time a new {nameof(Message)} is added or removed."),
                    SyntaxFactory.Comment("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated."),
                    SyntaxFactory.Comment("// </auto-generated>"),
                    SyntaxFactory.Comment("//------------------------------------------------------------------------------"),
                    SyntaxFactory.ElasticCarriageReturnLineFeed // add a blank line
                );
            }


            private void CreateFile(string fileContent, string outputPath)
            {
                if (!System.IO.Directory.Exists(MessageBrokerGenerator._outputFolder))
                {
                    System.IO.Directory.CreateDirectory(MessageBrokerGenerator._outputFolder);
                }

                System.IO.File.WriteAllText(outputPath, fileContent.ToString());
            }
            
            /// <remarks>Does not work as expected</remarks>
            public static SyntaxTriviaList CreateMultilineSummaryComment(string[] lines)
            {
                var contentList = new List<XmlNodeSyntax>();

                contentList.Add(SyntaxFactory.XmlText()
                    .AddTextTokens(
                        SyntaxFactory.XmlTextNewLine("\n", false),
                        SyntaxFactory.XmlTextLiteral(" ")
                    ));

                // Add each line of the summary
                foreach (var line in lines)
                {
                    contentList.Add(SyntaxFactory.XmlText(line));
                    contentList.Add(SyntaxFactory.XmlText()
                        .AddTextTokens(
                            SyntaxFactory.XmlTextNewLine("\n", false),
                            SyntaxFactory.XmlTextLiteral(" ")
                        ));
                }

                var summaryElement = SyntaxFactory.XmlElement(
                    SyntaxFactory.XmlElementStartTag(SyntaxFactory.XmlName("summary")),
                    SyntaxFactory.List(contentList),
                    SyntaxFactory.XmlElementEndTag(SyntaxFactory.XmlName("summary"))
                );

                return SyntaxFactory.TriviaList(
                    SyntaxFactory.DocumentationCommentExterior("/// "),
                    SyntaxFactory.Trivia(
                        SyntaxFactory.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia)
                            .AddContent(summaryElement)
                    )
                );
            }
            
            private MemberDeclarationSyntax GenerateEventHandlerEvent(MessageInfo messageInfo)
            {
                var efd = SyntaxFactory.EventFieldDeclaration(
                    SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.GenericName(
                            SyntaxFactory.Identifier("EventHandler"))
                        .WithTypeArgumentList(
                            SyntaxFactory.TypeArgumentList(
                                SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                    SyntaxFactory.IdentifierName("MessageBrokerEventArgs")))))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier("OnGameOver")))))
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(
                                SyntaxFactory.TriviaList(
                                    new[]
                                    {
                                        SyntaxFactory.Trivia(
                                            SyntaxFactory.RegionDirectiveTrivia(true)
                                                .WithEndOfDirectiveToken(
                                                    SyntaxFactory.Token(
                                                        SyntaxFactory.TriviaList(
                                                            SyntaxFactory.PreprocessingMessage("Event declaration")),
                                                        SyntaxKind.EndOfDirectiveToken,
                                                        SyntaxFactory.TriviaList()))),
                                        SyntaxFactory.Trivia(
                                            SyntaxFactory.DocumentationCommentTrivia(
                                                SyntaxKind.SingleLineDocumentationCommentTrivia,
                                                SyntaxFactory.List<XmlNodeSyntax>(
                                                    new XmlNodeSyntax[]
                                                    {
                                                        SyntaxFactory.XmlText()
                                                            .WithTextTokens(
                                                                SyntaxFactory.TokenList(
                                                                    SyntaxFactory.XmlTextLiteral(
                                                                        SyntaxFactory.TriviaList(
                                                                            SyntaxFactory.DocumentationCommentExterior("///")),
                                                                        " ",
                                                                        " ",
                                                                        SyntaxFactory.TriviaList()))),
                                                        SyntaxFactory.XmlExampleElement(
                                                            SyntaxFactory.SingletonList<XmlNodeSyntax>(
                                                                SyntaxFactory.XmlText()
                                                                    .WithTextTokens(
                                                                        SyntaxFactory.TokenList(
                                                                            new[]
                                                                            {
                                                                                SyntaxFactory.XmlTextNewLine(
                                                                                    SyntaxFactory.TriviaList(),
                                                                                    Environment.NewLine,
                                                                                    Environment.NewLine,
                                                                                    SyntaxFactory.TriviaList()),
                                                                                SyntaxFactory.XmlTextLiteral(
                                                                                    SyntaxFactory.TriviaList(
                                                                                        SyntaxFactory.DocumentationCommentExterior(
                                                                                            "        ///")),
                                                                                    " Description for onGameOver",
                                                                                    " Description for onGameOver",
                                                                                    SyntaxFactory.TriviaList()),
                                                                                SyntaxFactory.XmlTextNewLine(
                                                                                    SyntaxFactory.TriviaList(),
                                                                                    Environment.NewLine,
                                                                                    Environment.NewLine,
                                                                                    SyntaxFactory.TriviaList()),
                                                                                SyntaxFactory.XmlTextLiteral(
                                                                                    SyntaxFactory.TriviaList(
                                                                                        SyntaxFactory.DocumentationCommentExterior(
                                                                                            "        ///")),
                                                                                    " ",
                                                                                    " ",
                                                                                    SyntaxFactory.TriviaList())
                                                                            }))))
                                                            .WithStartTag(
                                                                SyntaxFactory.XmlElementStartTag(
                                                                    SyntaxFactory.XmlName(
                                                                        SyntaxFactory.Identifier("summary"))))
                                                            .WithEndTag(
                                                                SyntaxFactory.XmlElementEndTag(
                                                                    SyntaxFactory.XmlName(
                                                                        SyntaxFactory.Identifier("summary")))),
                                                        SyntaxFactory.XmlText()
                                                            .WithTextTokens(
                                                                SyntaxFactory.TokenList(
                                                                    SyntaxFactory.XmlTextNewLine(
                                                                        SyntaxFactory.TriviaList(),
                                                                        Environment.NewLine,
                                                                        Environment.NewLine,
                                                                        SyntaxFactory.TriviaList())))
                                                    })))
                                    }),
                                SyntaxKind.PublicKeyword,
                                SyntaxFactory.TriviaList())));
                return null;
            }

            public MethodDeclarationSyntax GenerateSendCharacterCreatedMethod(string methodName)
            {
                // Define method parameters
                var senderParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("sender"))
                    .WithType(SyntaxFactory.ParseTypeName("object"));

                var targetParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("target"))
                    .WithType(SyntaxFactory.ParseTypeName("object"));

                var characterStatsParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("characterStatsInstance"))
                    .WithType(SyntaxFactory.ParseTypeName("DnD.Code.Scripts.Characters.CharacterStats"));

                // Define method signature
                var method = SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), 
                        "Send_OnCharacterCreated")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddParameterListParameters(senderParameter, targetParameter, characterStatsParameter);

                // Create: if (sender == null)
                var ifCondition = SyntaxFactory.IfStatement(
                    SyntaxFactory.BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        SyntaxFactory.IdentifierName("sender"),
                        SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)),
                    SyntaxFactory.Block(
                        SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.InvocationExpression(
                                SyntaxFactory.MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    SyntaxFactory.IdentifierName("Debug"),
                                    SyntaxFactory.IdentifierName("LogError")))
                                .AddArgumentListArguments(
                                    SyntaxFactory.Argument(
                                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("sender is required."))))),
                        SyntaxFactory.ReturnStatement()
                    )
                );

                // Create: CharacterCreated?.Invoke(sender, target, characterStatsInstance)
                var invokeStatement = SyntaxFactory.ExpressionStatement(
                    SyntaxFactory.ConditionalAccessExpression(
                        SyntaxFactory.IdentifierName(methodName),
                        SyntaxFactory.InvocationExpression(SyntaxFactory.IdentifierName("Invoke"))
                            .AddArgumentListArguments(
                                SyntaxFactory.Argument(SyntaxFactory.IdentifierName("sender")),
                                SyntaxFactory.Argument(SyntaxFactory.IdentifierName("target")),
                                SyntaxFactory.Argument(SyntaxFactory.IdentifierName("characterStatsInstance"))
                            )
                    )
                );

                // Define method body
                var methodBody = SyntaxFactory.Block(ifCondition, invokeStatement);

                // Add the body to the method and return it
                return method.WithBody(methodBody);
            }

        }
    }
}