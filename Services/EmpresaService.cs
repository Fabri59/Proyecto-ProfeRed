using Proyecto_Red.Interfaces;
using Proyecto_Red.Models;

namespace Proyecto_Red.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmpresaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Empresa empresa)
        {
            await _unitOfWork.Repository<Empresa>().AddAsync(empresa);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var empresa = await _unitOfWork.Repository<Empresa>().GetByIdAsync(id);
            if (empresa is null) return;
            _unitOfWork.Repository<Empresa>().Remove(empresa);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Empresa>().GetAllAsync();
        }

        public async Task<Empresa?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Empresa>().GetByIdAsync(id);
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _unitOfWork.Repository<Empresa>().Update(empresa);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
