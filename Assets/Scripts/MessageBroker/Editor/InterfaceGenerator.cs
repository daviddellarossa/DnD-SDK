﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis; // Core Roslyn APIs
using Microsoft.CodeAnalysis.CSharp; // C#-specific APIs
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerGenerator
    {
        internal class InterfaceGenerator
        {
            private readonly string[] _categories;
            
            public InterfaceGenerator(string[] categories)
            {
                this._categories = categories;
            }
            
            internal void Generate()
            {
                var messageBrokerInterface = GenerateMessageBrokerInterface(_categories);
                
                var usings = GetUsings();
                var @namespace = NamespaceDeclaration(ParseName(_namespace))
                    .AddMembers(messageBrokerInterface);
                
                var compilationUnit = CompilationUnit()
                    .AddUsings(usings)
                    .AddMembers(@namespace)
                    .NormalizeWhitespace();
                
                var outputPath = System.IO.Path.Combine(_outputFolder, InterfaceFilename);
                this.CreateFile(compilationUnit.ToFullString(), outputPath);
            }
            
            private static string InterfaceName => $"I{nameof(MessageBroker)}";
            private static string InterfaceFilename => $"{InterfaceName}.cs";
            
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
                    UsingDirective(ParseName("MessageBroker")),
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

            private InterfaceDeclarationSyntax GenerateMessageBrokerInterface(string[] categories)
            {
                var @interface = InterfaceDeclaration(InterfaceName)
                    .WithModifiers(GetSummary())
                    .WithMembers(
                        List(
                            GetProperties(categories.ToArray())));
                
                return @interface;
            }

            private IEnumerable<MemberDeclarationSyntax> GetProperties(string[] categories)
            {
                var properties = new List<MemberDeclarationSyntax>();
                foreach (var category in categories)
                {
                    var summaryMessage = $"Message Broker for `{category}` category";
                    var propertyTypeString = $"{_categoryPrefix}{category}";
                    var propertyName = $"{category}";
                    
                    var propertyDeclaration = PropertyDeclaration(
                            IdentifierName(
                                Identifier(
                                    TriviaList(
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
                                                                                                "\t\t///")),
                                                                                        $" {summaryMessage}",
                                                                                        $" {summaryMessage}",
                                                                                        TriviaList()),
                                                                                    XmlTextNewLine(
                                                                                        TriviaList(),
                                                                                        Environment.NewLine,
                                                                                        Environment.NewLine,
                                                                                        TriviaList()),
                                                                                    XmlTextLiteral(
                                                                                        TriviaList(
                                                                                            DocumentationCommentExterior(
                                                                                                "\t\t///")),
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
                                                    })))),
                                    propertyTypeString,
                                    TriviaList())),
                            Identifier(propertyName))
                        .WithAccessorList(
                            AccessorList(
                                SingletonList(
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)))));
                    
                    properties.Add(propertyDeclaration);
                }
                
                return properties;
            }

            private SyntaxTokenList GetSummary()
            {
                var summary = TokenList(
                    new[]
                    {
                        Token(
                            TriviaList(
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
                                                                                        "\t///")),
                                                                                " Interface for the Message Broker component.",
                                                                                " Interface for the Message Broker component.",
                                                                                TriviaList()),
                                                                            XmlTextNewLine(
                                                                                TriviaList(),
                                                                                Environment.NewLine,
                                                                                Environment.NewLine,
                                                                                TriviaList()),
                                                                            XmlTextLiteral(
                                                                                TriviaList(
                                                                                    DocumentationCommentExterior(
                                                                                        "\t///")),
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
                                            })))),
                            SyntaxKind.PublicKeyword,
                            TriviaList()),
                        Token(SyntaxKind.PartialKeyword)
                    });
                
                return summary;
            }
        }
    }
}