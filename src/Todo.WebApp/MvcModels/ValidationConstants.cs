using System;

namespace Todo.WebApp.MvcModels
{
    public static class ValidationConstants
    {
        public const int MinStrLen = 3;
        public const string MinStrError = "Must be at least 3 characters";
        public const int MaxStrLen = 1000;
        public const string MaxStrError = "Cannot exceed 1000 characters";

        public const int MinListLen = 1;
        public const string MinListError = "Must have at least 1 item";
        public const int MaxListLen = 500;
        public const string MaxListError =  "Cannot exceed 500 items";
    }
}
