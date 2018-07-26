namespace AuthorizeNet.Util
{
    public class SensitiveTag
    {
        public string tagName { get; set; }
        public string pattern { get; set; }
        public string replacement { get; set; }
        public bool disableMask { get; set; }

        public SensitiveTag(string tagName, string pattern, string replacement, bool disableMask)
        {
            this.tagName = tagName;
            this.pattern = pattern;
            this.replacement = replacement;
            this.disableMask = disableMask;
        }
    }

    public static class SensitiveDataConfigType
    {
        public static SensitiveTag[] sensitiveTags = new SensitiveTag[]
        {
            new SensitiveTag("cardCode", "", "XXX", false),
            new SensitiveTag("cardNumber", "(\\p{N}+)(\\p{N}{4})", "XXXX-$2", false),
            new SensitiveTag("expirationDate", "", "XXX", false),
            new SensitiveTag("accountNumber", "(\\p{N}+)(\\p{N}{4})", "XXXX-$2", false),
            new SensitiveTag("nameOnAccount", "", "XXX", false),
            new SensitiveTag("transactionKey", "", "XXX", false)
        };

        public static string[] sensitiveStringRegexes = new string[] {
            "4\\p{N}{3}([\\ \\-]?)\\p{N}{4}\\1\\p{N}{4}\\1\\p{N}{4}",
            "4\\p{N}{3}([\\ \\-]?)(?:\\p{N}{4}\\1){2}\\p{N}(?:\\p{N}{3})?",
            "5[1-5]\\p{N}{2}([\\ \\-]?)\\p{N}{4}\\1\\p{N}{4}\\1\\p{N}{4}",
            "6(?:011|22(?:1(?=[\\ \\-]?(?:2[6-9]|[3-9]))|[2-8]|9(?=[\\ \\-]?(?:[01]|2[0-5])))|4[4-9]\\p{N}|5\\p{N}\\p{N})([\\ \\-]?)\\p{N}{4}\\1\\p{N}{4}\\1\\p{N}{4}",
            "35(?:2[89]|[3-8]\\p{N})([\\ \\-]?)\\p{N}{4}\\1\\p{N}{4}\\1\\p{N}{4}",
            "3[47]\\p{N}\\p{N}([\\ \\-]?)\\p{N}{6}\\1\\p{N}{5}"};
    }
}
