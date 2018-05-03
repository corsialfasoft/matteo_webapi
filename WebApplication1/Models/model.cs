using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1;

namespace DraxManUC001.Models {
	interface IDomainModel{
		Prodotto Search(int id);
		List<Prodotto> Search(string des);
		void SpedisciOrdine(List<Prodotto> prodotti);
		void AggiungiProdotto(Prodotto input);
		void RimuoviProdotto(int id);
	}

	public class Prodotto {
		public int Id{ get;set;}
		public string Descrizione{ get;set;}
		public int Giacenza{ get;set;}
		public int QtaOrdine{ get;set;}
        public override bool Equals(object obj) {
            return this.Id==((Prodotto)obj).Id;
        }

		public override int GetHashCode() {
			var hashCode = 371854915;
			hashCode = hashCode * -1521134295 + Id.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Descrizione);
			hashCode = hashCode * -1521134295 + Giacenza.GetHashCode();
			hashCode = hashCode * -1521134295 + QtaOrdine.GetHashCode();
			return hashCode;
		}
	}

	public class DomainModel : IDomainModel {
		RICHIESTEEntities db = null;
		public void AggiungiProdotto(Prodotto input){
			using(db= new RICHIESTEEntities()){
				db.AggiungiProdotto(input.Descrizione,input.Giacenza);	
			}//
		}
		public void RimuoviProdotto(int id){
			using(db= new RICHIESTEEntities()){
				db.RimuoviProdotto(id);	
			}
		}
		public Prodotto Search(int id) {
			using(db= new RICHIESTEEntities()){
				ProdottiSet prodotto = db.ProdottiSet.Find(id);
				if(prodotto!=null){
					return new Prodotto{ Id=prodotto.Id,Descrizione=prodotto.descrizione,Giacenza=prodotto.quantita};
				}
				return null;
			}
		}
	
		public List<Prodotto> Search(string des) {
			using (db = new RICHIESTEEntities()) {
				var query = from pro in db.ProdottiSet
							where pro.descrizione.Contains(des)
							select pro;
				List<Prodotto> prodotti = new List<Prodotto>();
 				foreach(ProdottiSet prodotto in query)
					prodotti.Add(new Prodotto { Id = prodotto.Id, Descrizione = prodotto.descrizione, Giacenza = prodotto.quantita });
				return prodotti;
			}
		}

		public void SpedisciOrdine(List<Prodotto> prodotti) {
			using (db = new RICHIESTEEntities()) {
				RichiesteSet richiesta = new RichiesteSet{data=DateTime.Now};
				db.RichiesteSet.Add(richiesta);
				foreach(Prodotto prodotto in prodotti){
					ProdottiSet ps = db.ProdottiSet.Find(prodotto.Id);
					if(ps!=null){ 
						RichiesteProdotti rp =new RichiesteProdotti{RichiesteSet = richiesta, ProdottiSet = ps, quantita= prodotto.QtaOrdine};
						db.RichiesteProdotti.Add(rp);
						db.SaveChanges();
					}
				}
			}
		}
	}
}