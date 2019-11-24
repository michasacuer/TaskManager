namespace TaskManager.Entity.JSONMapper
{
    using System.Collections.Generic;
    using TaskManager.Entity.Base;
    
    public abstract class JSONResponse<T>
        where T : BaseEntity<int>
    {
        public List<T> List { get; set; }
    }
}
