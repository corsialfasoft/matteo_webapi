using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using  DraxManUC001.Models ;
namespace WebApplication1.Controllers {
	public class ProdottiController : ApiController {
		IDomainModel dm = new DomainModel();
		// GET api/values
		public IEnumerable<Prodotto> Get() {
			return dm.Search("");
		}

		// GET api/values/5
		public Prodotto Get(int id) {
			return dm.Search(id);
		}

		// POST api/values
		public void Post([FromBody]Prodotto input) {
			dm.AggiungiProdotto(input);
		}

		// PUT api/values/5
		public void Put(int id,[FromBody]string value) {
		}

		// DELETE api/values/5
		public void Delete(int id) {
			dm.RimuoviProdotto(id);
		}
	}
}
