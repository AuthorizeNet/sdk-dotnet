﻿namespace AuthorizeNet.Util
{
    using System;
    using System.ComponentModel;

#pragma warning disable 1591
    //@deprecated since version 1.9.8  
    //@deprecated Since it is not using by New model Code
    [Obsolete("Since the classes using it are deprecated", false)]
    public class EnumHelper
    {

        public static string GetEnumDescription(Enum value)
        {
            string description = value.ToString();

            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (null != attributes && attributes.Length > 0)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                description = attributes[0].Description;
            }

            return description;
        }
    }
#pragma warning restore 1591
}
