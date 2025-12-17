namespace OrderService.Service
{
    public interface IService<Treceive, TSend>
    {
        Task<TSend> Create(Treceive receive);
        Task<TSend> Update(Treceive receive, int id);
        bool Delete(int id);
        Task<TSend> GetById(int id);
        Task<List<TSend>> GetAll();

    }
}
