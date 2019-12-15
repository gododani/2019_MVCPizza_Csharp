using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using TobbbformosPizzaAlkalmazasEgyTabla.Model;

namespace TobbbformosPizzaAlkalmazasEgyTabla.Repository
{
    partial class Repository
    {
        List<Futar> futarok;

        public List<Futar> getFutarok()
        {
            return futarok;
        }

        public void setFutarok(List<Futar> futarok)
        {
            this.futarok = futarok;
        }
        public DataTable getFutarDataTableFromList()
        {
            DataTable futarDT = new DataTable();
            futarDT.Columns.Add("id", typeof(int));
            futarDT.Columns.Add("nev", typeof(string));
            futarDT.Columns.Add("lakcim", typeof(string));
            futarDT.Columns.Add("telefonszam", typeof(string));
            futarDT.Columns.Add("email", typeof(string));
            foreach (Futar f in futarok)
            {
                futarDT.Rows.Add(f.getId(), f.getName(), f.getAddress(), f.getPhonenumber(), f.getEmail());
            }
            return futarDT;
        }
        private void fillFutarListFromDataTable(DataTable futardt)
        {
            foreach (DataRow row in futardt.Rows)
            {
                int azon = Convert.ToInt32(row[0]);
                string nev = row[1].ToString();
                string lakcim = row[2].ToString();
                string telefonszam = row[3].ToString();
                string email = row[4].ToString();
                Futar f = new Futar(azon, nev, lakcim, telefonszam, email);
                futarok.Add(f);
            }
        }
        public void deleteFutarFromList(int id)
        {
            Futar f = futarok.Find(x => x.getId() == id);
            if (f != null)
                futarok.Remove(f);
            else
                throw new RepositoryExceptionCantDelete("A futárt nem lehetett törölni.");
        }
        public void updateFutarInList(int id, Futar modified)
        {
            Futar f = futarok.Find(x => x.getId() == id);
            if (f != null)
                f.update(modified);
            else
                throw new RepositoryExceptionCantModified("A futár módosítása nem sikerült");
        }
        public void addFutarToList(Futar ujFutar)
        {
            try
            {
                futarok.Add(ujFutar);
            }
            catch (Exception e)
            {
                throw new RepositoryExceptionCantAdd("A futár hozzáadása nem sikerült");
            }
        }
        public Futar getFutar(int id)
        {
            return futarok.Find(x => x.getId() == id);
        }

        public int getNextFutarId()
        {
            if (futarok.Count == 0)
                return 1;
            else
                return futarok.Max(x => x.getId()) + 1;
        }
    }
}
