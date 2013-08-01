using System;
using System.Collections.Generic;
using System.Reflection;
using OpenOptions.dnaFusion.Flex.V1.InstantSearchKeywords;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public abstract class AInstantSearch
    {
        static AInstantSearch()
        {
            Keywords = new Dictionary<string, AKeyword>();
            LoadKeywords();
        }

        public string[] Criteria { get; protected set; }

        public bool HasKeyword { get; protected set; }

        public AKeyword Keyword { get; protected set; }

        public bool HasMember { get; protected set; }

        public string[] Member { get; protected set; }

        public string SearchText
        {
            get
            {
                if (HasKeyword)
                {
                    if (HasMember)
                        return string.Format("{0}:{1};{2}", Keyword, string.Join("|", Criteria), string.Join("|", Member));
                    return string.Format("{0}:{1}", Keyword, string.Join("|", Criteria));
                }
                else
                    return string.Join("|", Criteria);
            }
        }

        public static Dictionary<string, AKeyword> Keywords { get; private set; }

        private static void AddKeyword(Type keyword, Dictionary<string, AKeyword> dictionary)
        {
            AKeyword item = Activator.CreateInstance(keyword) as AKeyword;
            if (item != null)
                dictionary.Add(item.Keyword, item);
        }

        public static AKeyword Get(string keyword)
        {
            if (Keywords.ContainsKey(keyword))
                return Keywords[keyword];
            return null;
        }

        private static void LoadKeywords()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (type.IsSubclassOf(typeof(AKeyword)) && type.Namespace == "OpenOptions.dnaFusion.Flex.V1.InstantSearchKeywords")
                    AddKeyword(type, Keywords);
        }
    }
}
