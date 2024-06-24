using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{

    public class ContractManagementService : IContractManagementService
    {
        public async Task<ContractDTO> AddContractAsync(CreateContractDTO createContractDto)
        {
              var contractId = GenerateContractId();
            var contract = new ContractDTO
            {
                Id = contractId,
                SupplierId = createContractDto.SupplierId,
                StartDate = createContractDto.StartDate,
                EndDate = createContractDto.EndDate,
                Terms = createContractDto.Terms
            };

            return contract;
        }

        private int GenerateContractId()
        {
            return 1; 
        }
    }
}