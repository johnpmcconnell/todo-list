using System;

namespace Todo.WebApp
{
    // internal since these are const

    /// <summary>
    /// Contains route names for actions that return HTML pages.
    /// Only actions that have code references are given explicit names.
    /// </summary>
    internal static class HtmlRouteActionNames
    {
        public const string TodoListGet = "TodoListGet";
    }

    /// <summary>
    /// Contains route names for actions that return serialized objects.
    /// Only actions that have code references are given explicit names.
    /// </summary>
    internal static class ApiRouteActionNames
    {
        public const string TodoListGet = "Api" + HtmlRouteActionNames.TodoListGet;
    }
}
