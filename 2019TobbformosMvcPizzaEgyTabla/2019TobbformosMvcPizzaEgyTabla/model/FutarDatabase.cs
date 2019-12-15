using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobbbformosPizzaAlkalmazasEgyTabla.Model
{
    partial class Futar
    {
        public string getInsertFutar()
        {
            return
                "INSERT INTO `futarok` (`id`, `nev`, `lakcim`, `telefonszam`, `email`) " +
                "VALUES ('" +
                id +
                "', '" +
                getName() +
                "', '" +
                getAddress() +
                "', '" +
                getPhonenumber() +
                "', '" +
                getEmail() +
                "');";
        }

        public string getUpdateFutar(int id)
        {
            return
                "UPDATE `futarok` SET `nev` = '" +
                getName() +
                "', `lakcim` = '" +
                getAddress() +
                "', `telefonszam` = '" +
                getPhonenumber() +
                "', `email` = '" +
                getEmail() +
                "' WHERE `futarok`.`id` = " +
                id;
        }

        public static string getSQLCommandDeleteAllRecord()
        {
            return "DELETE FROM futarok";
        }

        public static string getSQLCommandGetAllRecord()
        {
            return "SELECT * FROM futarok";
        }
    }
}
