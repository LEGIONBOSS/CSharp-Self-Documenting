using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SelfDocumenting
{
    static class MDDocumentService
    {
        private static string GetFriendlyName(object obj)
        {
            return GetFriendlyName(obj.GetType());
        }

        private static string GetFriendlyName(Type type)
        {
            if (type.FullName.Contains("System."))
            {
                string name = new CSharpCodeProvider().GetTypeOutput(new CodeTypeReference(type));
                return name.Substring(name.LastIndexOf(".") + 1);
            }
            else
            {
                return type.Name;
            }
        }

        public static string MakeMDDocumentation(object obj)
        {
            return MakeMDDocumentation(obj.GetType());
        }

        public static string MakeMDDocumentation(Type type)
        {
            string name = type.Name;
            string nl = Environment.NewLine;
            string top = "> [Back to top](#top)";

            // Title
            string res = $"# {name}.cs{nl}";

            // TOC
            res += $"{nl}## Table of contents{nl}";
            res += $"1. [Fields](#fields){nl}";
            res += $"2. [Properties](#properties){nl}";
            res += $"3. [Methods](#methods){nl}";

            // Fields
            res += $"{nl}## Fields{nl}";
            res += $"{nl}| Name | Type | Meaning |{nl}";
            res += $"| --- | --- | --- |{nl}";
            foreach (FieldInfo item in type.GetFields())
            {
                res += $"| {item.Name} | {GetFriendlyName(item.FieldType)} | ??? |{nl}";
            }
            res += $"{nl}{top}{nl}";

            // Props
            res += $"{nl}## Properties{nl}";
            res += $"{nl}| Name | Type | Meaning |{nl}";
            res += $"| --- | --- | --- |{nl}";
            foreach (PropertyInfo item in type.GetProperties())
            {
                res += $"| {item.Name} | {GetFriendlyName(item.PropertyType)} | ??? |{nl}";
            }
            res += $"{nl}{top}{nl}";

            // Methods
            res += $"{nl}## Methods{nl}";
            res += $"{nl}| Name | Type | Parameters | Meaning |{nl}";
            res += $"| --- | --- | --- | --- |{nl}";
            foreach (MethodInfo item in type.GetMethods().Where(m => !m.IsSpecialName))
            {
                List<string> pars = new List<string>();
                foreach (ParameterInfo item2 in item.GetParameters())
                {
                    pars.Add($"{GetFriendlyName(item2.ParameterType)} {item2.Name}");
                }
                res += $"| {item.Name} | {GetFriendlyName(item.ReturnType)} | {string.Join(", ", pars)} | ??? |{nl}";
            }
            res += $"{nl}{top}{nl}";

            return res;
        }
    }
}
