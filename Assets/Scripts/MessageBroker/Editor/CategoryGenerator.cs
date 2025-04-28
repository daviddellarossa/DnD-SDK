using System;
using System.CodeDom.Compiler;
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

                // TODO: Generate the Category class here
                
                var categoryClass = GenerateCategoryClass(this._categoryName, this._messageInfos.ToArray());
                
                var usings = GetUsings();
                var @namespace = NamespaceDeclaration(ParseName(MessageBrokerGenerator._namespace))
                    .AddMembers(classes.Cast<MemberDeclarationSyntax>().ToArray())
                    .AddMembers(categoryClass);
                
                var compilationUnit = CompilationUnit()
                    .AddUsings(usings)
                    .AddMembers(@namespace)
                    .NormalizeWhitespace();

                // var str = compilationUnit.ToFullString();
                
                var outputPath = System.IO.Path.Combine(MessageBrokerGenerator._outputFolder, $"{MessageBrokerGenerator.ClassName}_TestRoslyn.{MessageBrokerGenerator._categoryPrefix}{this._categoryName}.cs");

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

            internal SyntaxTriviaList GetSummary(string summary)
            {
                return TriviaList(
                    Trivia(
                        DocumentationCommentTrivia(
                            SyntaxKind.SingleLineDocumentationCommentTrivia,
                            List<XmlNodeSyntax>(
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

            internal ClassDeclarationSyntax GenerateCategoryClass(string categoryName, MessageInfo[] messageInfos)
            {
                var newClassName = $"{MessageBrokerGenerator._categoryPrefix}{categoryName}";
                var newClass = ClassDeclaration(newClassName)
                        .WithModifiers(
                            TokenList(
                                Token(
                                    GetSummary(string.Empty),
                                    SyntaxKind.PublicKeyword,
                                    TriviaList())))
                        // Add events
                        .AddMembers(messageInfos.Select(x =>
                        {
                            return EventFieldDeclaration(
                                    VariableDeclaration(
                                            GenericName(
                                                    Identifier(nameof(EventHandler)))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName(GetEventArgsClassName(x))))))
                                        .WithVariables(
                                            SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                VariableDeclarator(
                                                    Identifier(GetEventName(x)))))
                                )
                                .WithModifiers(
                                    TokenList(
                                        Token(
                                            GetSummary(x.Message.EventComment),
                                            SyntaxKind.PublicKeyword,
                                            TriviaList())));
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

            internal IfStatementSyntax CheckForNull(InputParameter inputParameter)
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
                                    SingletonSeparatedList<VariableDeclaratorSyntax>(
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
            
            internal MethodDeclarationSyntax GenerateSendMethod(MessageInfo messageInfo)
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
                                .Where(x => !x.IsNullable)
                                .Select(x =>
                                {
                                    return CheckForNull(x);
                                })
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
                                                SingletonSeparatedList<VariableDeclaratorSyntax>(
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
                    var newClass = ClassDeclaration(newClassName)
                        .WithModifiers(
                            TokenList(
                                Token(
                                    GetSummary(string.Empty),
                                    SyntaxKind.PublicKeyword,
                                    TriviaList())))
                        .AddBaseListTypes(new[]
                        {
                            SimpleBaseType(ParseTypeName(nameof(MessageBrokerEventArgs))),
                            SimpleBaseType(ParseTypeName(nameof(IResettable)))
                        })
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
                                            List<XmlNodeSyntax>(
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
            
            internal static string GetParameterType(InputParameter inputParameter)
            {
                return inputParameter.ParameterType switch
                {
                    ParameterType.BooleanType => Token(SyntaxKind.BoolKeyword).ValueText,
                    ParameterType.ByteType => Token(SyntaxKind.ByteKeyword).ValueText,
                    ParameterType.DoubleType => Token(SyntaxKind.DoubleKeyword).ValueText,
                    ParameterType.FloatType => Token(SyntaxKind.FloatKeyword).ValueText,
                    ParameterType.IntType => Token(SyntaxKind.IntKeyword).ValueText,
                    ParameterType.LongType => Token(SyntaxKind.LongKeyword).ValueText,
                    ParameterType.ShortType => Token(SyntaxKind.ShortKeyword).ValueText,
                    ParameterType.StringType => Token(SyntaxKind.StringKeyword).ValueText,
                    ParameterType.VoidType => Token(SyntaxKind.VoidKeyword).ValueText,
                    ParameterType.TransformType => QualifiedName(IdentifierName("UnityEngine"), IdentifierName("Transform")).ToFullString(),
                    ParameterType.ObjectType => Token(SyntaxKind.ObjectKeyword).ValueText,
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
                
                var @class = ClassDeclaration($"{MessageBrokerGenerator._categoryPrefix}{categoryName}" + "_Test")
                    .AddModifiers(Token(SyntaxKind.PublicKeyword));
                
                eventMembers.ForEach(x => @class = @class.AddMembers(x));
                
                // Add a blank line as trivia
                var blankLineTrivia = TriviaList(
                    ElasticCarriageReturnLineFeed, // Adds a blank line
                    ElasticCarriageReturnLineFeed
                );


                sendMethodsMembers.ForEach(x => @class = @class.AddMembers(x));

                    //.AddMembers(eventField, method);
                
                var @namespace = NamespaceDeclaration(ParseName(MessageBrokerGenerator._namespace))
                    .AddMembers(@class);
                
                // Attach the header trivia to the CompilationUnit
                var compilationUnit = CompilationUnit()
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
                    UsingDirective(ParseName("System"))
                        .WithUsingKeyword(
                            Token(
                                GetFileHeader(),
                                SyntaxKind.UsingKeyword,
                                TriviaList())),
                    UsingDirective(ParseName("UnityEngine")),
                    UsingDirective(ParseName("UnityEditor")),
                    UsingDirective(ParseName("MessageBroker")),
                };
            }

            private SyntaxTriviaList GetFileHeader()
            {
                return
                    TriviaList(
                        new[]
                        {
                            Comment("//------------------------------------------------------------------------------"),
                            Comment("// <auto-generated>"),
                            Comment("// Code auto-generated by CategoryGenerator version <version undefined>."),
                            Comment("// Re-run the generator every time a new Message is added or removed."),
                            Comment("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated."),
                            Comment("// </auto-generated>"),
                            Comment("//------------------------------------------------------------------------------")
                        });
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

                contentList.Add(XmlText()
                    .AddTextTokens(
                        XmlTextNewLine("\n", false),
                        XmlTextLiteral(" ")
                    ));

                // Add each line of the summary
                foreach (var line in lines)
                {
                    contentList.Add(XmlText(line));
                    contentList.Add(XmlText()
                        .AddTextTokens(
                            XmlTextNewLine("\n", false),
                            XmlTextLiteral(" ")
                        ));
                }

                var summaryElement = XmlElement(
                    XmlElementStartTag(XmlName("summary")),
                    List(contentList),
                    XmlElementEndTag(XmlName("summary"))
                );

                return TriviaList(
                    DocumentationCommentExterior("/// "),
                    Trivia(
                        DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia)
                            .AddContent(summaryElement)
                    )
                );
            }
            
            private MemberDeclarationSyntax GenerateEventHandlerEvent(MessageInfo messageInfo)
            {
                var efd = EventFieldDeclaration(
                    VariableDeclaration(
                        GenericName(
                            Identifier("EventHandler"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(
                                    IdentifierName("MessageBrokerEventArgs")))))
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(
                                Identifier("OnGameOver")))))
                    .WithModifiers(
                        TokenList(
                            Token(
                                TriviaList(
                                    new[]
                                    {
                                        Trivia(
                                            RegionDirectiveTrivia(true)
                                                .WithEndOfDirectiveToken(
                                                    Token(
                                                        TriviaList(
                                                            PreprocessingMessage("Event declaration")),
                                                        SyntaxKind.EndOfDirectiveToken,
                                                        TriviaList()))),
                                        Trivia(
                                            DocumentationCommentTrivia(
                                                SyntaxKind.SingleLineDocumentationCommentTrivia,
                                                List<XmlNodeSyntax>(
                                                    new XmlNodeSyntax[]
                                                    {
                                                        XmlText()
                                                            .WithTextTokens(
                                                                TokenList(
                                                                    XmlTextLiteral(
                                                                        TriviaList(
                                                                            DocumentationCommentExterior("///")),
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
                                                                                        DocumentationCommentExterior(
                                                                                            "        ///")),
                                                                                    " Description for onGameOver",
                                                                                    " Description for onGameOver",
                                                                                    TriviaList()),
                                                                                XmlTextNewLine(
                                                                                    TriviaList(),
                                                                                    Environment.NewLine,
                                                                                    Environment.NewLine,
                                                                                    TriviaList()),
                                                                                XmlTextLiteral(
                                                                                    TriviaList(
                                                                                        DocumentationCommentExterior(
                                                                                            "        ///")),
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
                                                    })))
                                    }),
                                SyntaxKind.PublicKeyword,
                                TriviaList())));
                return null;
            }

            public MethodDeclarationSyntax GenerateSendCharacterCreatedMethod(string methodName)
            {
                // Define method parameters
                var senderParameter = Parameter(Identifier("sender"))
                    .WithType(ParseTypeName("object"));

                var targetParameter = Parameter(Identifier("target"))
                    .WithType(ParseTypeName("object"));

                var characterStatsParameter = Parameter(Identifier("characterStatsInstance"))
                    .WithType(ParseTypeName("DnD.Code.Scripts.Characters.CharacterStats"));

                // Define method signature
                var method = MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.VoidKeyword)), 
                        "Send_OnCharacterCreated")
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddParameterListParameters(senderParameter, targetParameter, characterStatsParameter);

                // Create: if (sender == null)
                var ifCondition = IfStatement(
                    BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName("sender"),
                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                    Block(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Debug"),
                                    IdentifierName("LogError")))
                                .AddArgumentListArguments(
                                    Argument(
                                        LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("sender is required."))))),
                        ReturnStatement()
                    )
                );

                // Create: CharacterCreated?.Invoke(sender, target, characterStatsInstance)
                var invokeStatement = ExpressionStatement(
                    ConditionalAccessExpression(
                        IdentifierName(methodName),
                        InvocationExpression(IdentifierName("Invoke"))
                            .AddArgumentListArguments(
                                Argument(IdentifierName("sender")),
                                Argument(IdentifierName("target")),
                                Argument(IdentifierName("characterStatsInstance"))
                            )
                    )
                );

                // Define method body
                var methodBody = Block(ifCondition, invokeStatement);

                // Add the body to the method and return it
                return method.WithBody(methodBody);
            }

        }
    }
}