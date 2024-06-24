using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class CustomerRelationshipManagementService : ICustomerRelationshipManagementService
    {
        public async Task<CustomerDTO> AddCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            // Implementação da adição de clientes
            // Aqui você pode adicionar lógica para persistir os dados em um banco de dados, por exemplo.

            // Simulação de adição de cliente
            var customerId = GenerateCustomerId();
            var customer = new CustomerDTO
            {
                Id = customerId,
                Name = createCustomerDTO.Name,
                Email = createCustomerDTO.Email,
                PhoneNumber = createCustomerDTO.PhoneNumber
            };

            return customer;
        }

        private int GenerateCustomerId()
        {
            // Lógica para gerar um ID de cliente único
            // Aqui você pode implementar um gerador de IDs único para cada novo cliente.
            return 1; // Simulação de ID único
        }
    }
}
