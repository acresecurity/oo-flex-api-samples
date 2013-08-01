using System;
using OpenOptions.dnaFusion.Flex.V1.InstantSearchKeywords;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class InstantSearch : AInstantSearch
    {
        #region ctor
        public InstantSearch(string criteria)
        {
            base.Criteria = criteria.Trim().Split('|');
            base.HasKeyword = false;
            base.HasMember = false;
            base.Member = new string[0];
            ParseCriteria(criteria);
        }

        public InstantSearch(AKeyword keyword, string criteria)
        {
            base.Criteria = criteria.Trim().Split('|');
            base.HasKeyword = true;
            base.Keyword = keyword;
            base.HasMember = false;
            base.Member = new string[0];
        }

        public InstantSearch(AKeyword keyword, string criteria, string member)
        {
            base.Criteria = criteria.Trim().Split('|');
            base.HasKeyword = true;
            base.Keyword = keyword;
            base.HasMember = true;
            base.Member = member.Trim().Split('|');
        }
        #endregion

        #region Parse
        //
        // Company:Open|Sec;Brown|Bell|Crawford
        //
        // Company = Keyword
        // Open|Sec = Criteria
        // Brown|Bell|Crawford = Member
        private void ParseCriteria(string Criteria)
        {
            if (string.IsNullOrEmpty(Criteria))
                return;

            foreach (AKeyword item in Keywords.Values)
            {
                string toMatch = string.Format("{0}:", item);
                HasKeyword = Criteria.StartsWith(toMatch, StringComparison.OrdinalIgnoreCase);
                if (HasKeyword)
                {
                    Keyword = item;
                    string criteria = Criteria.Substring(toMatch.Length).Trim();
                    this.Criteria = criteria.Split('|');
                    break;
                }
            }

            if (HasKeyword)
            {
                string criteria = string.Join("|", this.Criteria);
                int pos = criteria.IndexOf(';');
                HasMember = pos >= 0;
                if (HasMember)
                {
                    Member = criteria.Substring(pos + 1).Trim().Split('|');
                    this.Criteria = criteria.Substring(0, pos).Split('|');
                }
            }
        }
        #endregion
    }
}
