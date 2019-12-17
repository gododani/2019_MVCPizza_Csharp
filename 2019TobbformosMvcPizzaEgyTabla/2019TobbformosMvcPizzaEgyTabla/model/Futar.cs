using _2019TobbformosMvcPizzaEgyTabla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobbbformosPizzaAlkalmazasEgyTabla.Model
{
    partial class Futar
    {
        private int id;
        private string name;
        private string address;
        private string phonenumber;
        private string email;

        public Futar(int id, string name, string address, string phonenumber, string email)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phonenumber = phonenumber;
            this.email = email;

            if (!isValidName(name))
                throw new ModelFutarNotValidNameExeption("A futár neve nem megfelelő!");
            if (!isValidLakcim(address))
                throw new ModelFutarNotValidLakcimExeption("A futár lakcíme nem megfelelő!");
            if (!isValidTelefonszam(phonenumber))
                throw new ModelFutarNotValidTelefonszamExeption("A futár telefonszáma nem megfelelő!");
            if (!isValidEmail(email))
                throw new ModelFutarNotValidEmailExeption("A futár Email címe nem megfelelő!");
        }

        private bool isValidEmail(string email)
        {
            if (email == string.Empty)
                return false;
            return true;
        }

        private bool isValidTelefonszam(string phonenumber)
        {
            if (phonenumber.Length == 0)
                return false;
            for (int i = 0; i < phonenumber.Length; i++)
            {
                if (!char.IsNumber(phonenumber.ElementAt(i)))
                    return false;
            }
            return true;
        }

        private bool isValidLakcim(string address)
        {
            if (address == string.Empty)
                return false;
            if (!char.IsUpper(address.ElementAt(0)))
                return false;
            return true;
        }

        private bool isValidName(string name)
        {
            if (name == string.Empty)
                return false;
            if (!char.IsUpper(name.ElementAt(0)))
                return false;
            for (int i = 1; i < name.Length; i = i + 1)
                if (
                    !char.IsLetter(name.ElementAt(i))
                        &&
                    (!char.IsWhiteSpace(name.ElementAt(i)))

                    )
                    return false;
            return true;
        }

        public void update(Futar modified)
        {
            this.name = modified.getName();
            this.address = modified.getAddress();
            this.phonenumber = modified.getPhonenumber();
            this.email = modified.getEmail();
        }
        public void setID(int id)
        {
            this.id = id;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setAddress(string address)
        {
            this.address = address;
        }
        public void setPhonenumber(string phonenumber)
        {
            this.phonenumber = phonenumber;
        }
        public void setEmail(string email)
        {
            this.email = email;
        }
        public int getId()
        {
            return id;
        }
        public string getName()
        {
            return name;
        }
        public string getAddress()
        {
            return address;
        }
        public string getPhonenumber()
        {
            return phonenumber;
        }
        public string getEmail()
        {
            return email;
        }
    }
}