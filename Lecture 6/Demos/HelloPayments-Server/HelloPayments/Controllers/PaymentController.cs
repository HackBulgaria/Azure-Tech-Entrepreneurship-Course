using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Braintree;

namespace HelloPayments.Controllers
{
	[RoutePrefix("api/payment")]
	public class PaymentController : ApiController
	{
		private readonly Lazy<BraintreeGateway> gateway = new Lazy<BraintreeGateway>(() => new BraintreeGateway
		{
			Environment = Braintree.Environment.SANDBOX,
			MerchantId = "nkmgnf2hxnxjw6j8",
			PublicKey = "sdcz5msvphb34sqz",
			PrivateKey = "1452c5eb8cdd98070d0720998e8a8547"
		});

		[Route("token")]
		[HttpPost]
		public string GetToken([FromBody] string email)
		{
			ClientTokenRequest request = null;
			var id = this.GetCustomerId(email);
			if (id != null)
			{
				request = new ClientTokenRequest
				{
					CustomerId = id
				};
			}

			return this.gateway.Value.ClientToken.generate(request);
        }

		[Route("purchase")]
		[HttpPost]
		public PaymentResponse Purchase([FromBody] PaymentRequest request)
		{
			var transaction = new TransactionRequest
			{
				Amount = request.Amount * request.Price,
				Descriptor = new DescriptorRequest
				{
					Name = "techent*subscription",
					Phone = "(0888)123456",
					Url = "google.com"
				},
				Options = new TransactionOptionsRequest
				{
					SubmitForSettlement = true,
					StoreInVault = true
				},
				PaymentMethodNonce = request.Nonce
			};

			var id = this.GetCustomerId(request.Email);
			if (id == null)
			{
				transaction.Customer = new CustomerRequest
				{
					Email = request.Email
				};
			}
			else
			{
				transaction.CustomerId = id;
			}

			var result = this.gateway.Value.Transaction.Sale(transaction);
			return new PaymentResponse
			{
				IsSuccess = result.IsSuccess(),
				Errors = result.Errors?.DeepAll()?.Select(e => e.Message)
			};
		}

		private string GetCustomerId(string email)
		{
			var query = new CustomerSearchRequest();
			query.Email.Is(email);
			return this.gateway.Value.Customer.Search(query).OfType<Customer>().FirstOrDefault()?.Id;
		}
	}

	public class PaymentRequest
	{
		public string Nonce { get; set; }

		public string Email { get; set; }

		public int Amount { get; set; }

		public decimal Price { get; set; }
	}

	public class PaymentResponse
	{
		public bool IsSuccess { get; set; }

		public IEnumerable<string> Errors { get; set; }
	}
}