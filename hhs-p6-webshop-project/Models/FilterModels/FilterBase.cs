namespace hhs_p6_webshop_project.Models.FilterModels
{
    public abstract class FilterBase
    {
        public abstract string Name { get; }

        public override string ToString()
        {
            return $"({Name})";
        }
    }
}