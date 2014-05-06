using System.ComponentModel.DataAnnotations;

namespace IQMProjectEvolutionManager.Core.Enums
{
    /// <summary>
    /// Drop Down Type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Enums are self descriptive.")] 
    public enum MyManagedListItemType
    {
        #pragma warning disable 1591
        [Display(Name = "System: Paging Settings")]
        Paging
        #pragma warning restore 1591
    }
}