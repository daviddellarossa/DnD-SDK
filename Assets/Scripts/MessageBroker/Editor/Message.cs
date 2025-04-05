using System;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace MessageBroker.Editor
{
    [CreateAssetMenu(menuName = "DeeDeeR/MessageBroker/New Message", fileName = "NewMessage")]

    public class Message : ScriptableObject
    {
        [SerializeField]
        private string messageName;
        [SerializeField]
        private string messageCategory;
        [SerializeField]
        private string sendMethodComment;
        [SerializeField]
        private string eventComment;
        [SerializeField]
        private List<InputParameter> inputParameters;
        [SerializeField]
        private ReturnParameter returnParameter;

        public string MessageName
        {
            get => messageName;
            set => messageName = value;
        }

        public string MessageCategory
        {
            get => messageCategory;
            set => messageCategory = value;
        }

        public string SendMethodComment
        {
            get => sendMethodComment;
            set => sendMethodComment = value;
        }

        public string EventComment
        {
            get => eventComment;
            set => eventComment = value;
        }

        public List<InputParameter> InputParameters
        {
            get => inputParameters;
            set => inputParameters = value;
        }

        public ReturnParameter ReturnParameter
        {
            get => returnParameter;
            set => returnParameter = value;
        }
        
        public string GetName()
        {
            return String.IsNullOrWhiteSpace(messageName) ? name : messageName;
        }

        //public string GetCategory()
        //{
        //    var currentScriptGuid = AssetDatabase.FindAssets($"t:Script {nameof(MessageBrokerGenerator)}");
        //    var currentScriptLocation = AssetDatabase.GUIDToAssetPath(currentScriptGuid[0]);
        //    var currentFolder = currentScriptLocation.Substring(0, currentScriptLocation.LastIndexOf('/'));

            //    string[] guids = AssetDatabase.FindAssets($"{this.name} t:{typeof(Message).Name}", new[] { currentFolder });

            //    return "Default"; // this.Category;
        //}
    }

    [Serializable]
    public struct InputParameter
    {
        public string parameterName;
        public Multiplicity multiplicity;
        public ParameterType parameterType;
        public string otherType;
        public string parameterComment;
    }

    [Serializable]
    public struct ReturnParameter
    {
        public Multiplicity multiplicity;
        public ParameterType parameterType;
        public string otherType;
        public string parameterComment;
    }

    public enum ParameterType
    {
        VoidType,
        BooleanType,
        ByteType,
        ShortType,
        IntType,
        LongType,
        FloatType,
        DoubleType,
        StringType,
        ObjectType,
        TransformType,
        OtherType,
    }

    public enum Multiplicity
    {
        Single,
        Array,
        List,
        IList,
        Collection,
        ICollection,
        IEnumerable,
    }

    public static class ExtensionMethods
    {
        public static string ToTypeString(this Multiplicity multiplicity)
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
        public static string ToParameterTypeString(this ParameterType parameterType, ParameterDirection direction, bool isNullable, string otherType)
        {
            return parameterType switch
            {
                ParameterType.BooleanType => isNullable ? "bool?" : "bool",
                ParameterType.ByteType => isNullable ? "byte?" : "byte",
                ParameterType.DoubleType => isNullable ? "double?" : "double",
                ParameterType.IntType => isNullable ? "int?" : "int",
                ParameterType.FloatType => isNullable ? "float?" : "float",
                ParameterType.LongType => isNullable ? "long?" : "long",
                ParameterType.ObjectType => isNullable ? "object?" : "object",
                ParameterType.ShortType => isNullable ? "short?" : "short",
                ParameterType.StringType => "string",
                ParameterType.TransformType => "UnityEngine.Transform",
                ParameterType.OtherType => isNullable ? $"{otherType}?" : otherType,
                ParameterType.VoidType => direction == ParameterDirection.Input ? throw new ArgumentException("Void type not valid for input parameter") : "void",
                _ => throw new NotSupportedException("Type not supported")
            };
        }
    }
}