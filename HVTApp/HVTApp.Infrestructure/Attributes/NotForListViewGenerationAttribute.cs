using System;

namespace HVTApp.Infrastructure.Attributes
{
    /// <summary>
    /// �������, ������� ���������� ������ �� ��������������� ��� ������������ ListViews
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NotForListViewGenerationAttribute : Attribute
    {
    }
}