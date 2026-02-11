namespace Shared.RequestParameters
{
    public abstract class CammonParameters
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        protected CammonParameters():this(1,6)
        {
            
        }

        protected CammonParameters(int pageNumber=1, int pageSize=6)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }
}
