using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TobbbformosPizzaAlkalmazasEgyTabla.Repository;
using TobbbformosPizzaAlkalmazasEgyTabla.Model;
using System.Diagnostics;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    public partial class FormPizzaFutarKft : Form
    {
        /// <summary>
        /// Pizzákat tartalmazó adattábla
        /// </summary>
        private DataTable futarsDT = new DataTable();
        /// <summary>
        /// Tárolja a pizzákat listában
        /// </summary>
        private Repository futarRepo = new Repository();

        bool ujFutarAdatfelvitel = false;

        private void buttonFutarBetoltes_Click(object sender, EventArgs e)
        {
            //Adatbázisban pizza tábla kezelése
            RepositoryDatabaseTablePizza rtf = new RepositoryDatabaseTablePizza();
            //A repo-ba lévő pizza listát feltölti az adatbázisból
            futarRepo.setFutarok(rtf.getFutarokFromDatabaseTable());
            frissitFutarAdatokkalDataGriedViewt();
            bealliFutartPizzaDataGriViewt();
            beallitFutarGombokatIndulaskor();

            dataGridViewFutars.SelectionChanged += DataGridViewFutars_SelectionChanged;
        }

        private void beallitFutarGombokatIndulaskor()
        {
            panelFutar.Visible = false;
            panelModositTorol.Visible = false;
            if (dataGridViewFutars.SelectedRows.Count != 0)
                buttonUjFutar.Visible = false;
            else
                buttonUjFutar.Visible = true;
        }

        private void DataGridViewFutars_SelectionChanged(object sender, EventArgs e)
        {

            if (ujFutarAdatfelvitel)
            {
                beallitFutarGombokatKattintaskor();
            }
            if (dataGridViewFutars.SelectedRows.Count == 1)
            {
                panelFutar.Visible = true;
                panelModositTorol.Visible = true;
                buttonUjFutar.Visible = true;
                textBoxFutarAzon.Text =
                    dataGridViewFutars.SelectedRows[0].Cells[0].Value.ToString();
                textBoxFutarNAME.Text =
                    dataGridViewFutars.SelectedRows[0].Cells[1].Value.ToString();
                textBoxFutarADDRESS.Text =
                    dataGridViewFutars.SelectedRows[0].Cells[2].Value.ToString();
                textBoxFutarPHONENUM.Text =
                    dataGridViewFutars.SelectedRows[0].Cells[3].Value.ToString();
                textBoxFutarEMAIL.Text =
                   dataGridViewFutars.SelectedRows[0].Cells[4].Value.ToString();
            }
            else
            {
                panelFutar.Visible = false;
                panelModositTorol.Visible = false;
                buttonUjFutar.Visible = false;
            }
        }

        private void frissitFutarAdatokkalDataGriedViewt()
        {
            //Adattáblát feltölti a repoba lévő pizza listából
            futarsDT = futarRepo.getFutarDataTableFromList();
            //Pizza DataGridView-nak a forrása a pizza adattábla
            dataGridViewFutars.DataSource = null;
            dataGridViewFutars.DataSource = futarsDT;
        }

        private void bealliFutartPizzaDataGriViewt()
        {
            futarsDT.Columns[0].ColumnName = "Azonosító";
            futarsDT.Columns[0].Caption = "Futár azonosító";
            futarsDT.Columns[1].ColumnName = "Név";
            futarsDT.Columns[1].Caption = "Futár név";
            futarsDT.Columns[2].ColumnName = "Lakcím";
            futarsDT.Columns[2].Caption = "Futár lakcím";
            futarsDT.Columns[3].ColumnName = "Telefonszám";
            futarsDT.Columns[3].Caption = "Futár telefonszám";
            futarsDT.Columns[4].ColumnName = "Email ";
            futarsDT.Columns[4].Caption = "Futár email";

            dataGridViewFutars.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFutars.ReadOnly = true;
            dataGridViewFutars.AllowUserToDeleteRows = false;
            dataGridViewFutars.AllowUserToAddRows = false;
            dataGridViewFutars.MultiSelect = false;
        }

        private void buttonTorolFutar_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            if ((dataGridViewFutars.Rows == null) ||
                (dataGridViewFutars.Rows.Count == 0))
                return;
            //A felhasználó által kiválasztott sor a DataGridView-ban            
            int sor = dataGridViewFutars.SelectedRows[0].Index;
            if (MessageBox.Show(
                "Valóban törölni akarja a sort?",
                "Törlés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //1. törölni kell a listából
                int id = -1;
                if (!int.TryParse(
                         dataGridViewFutars.SelectedRows[0].Cells[0].Value.ToString(),
                         out id))
                    return;
                try
                {
                    futarRepo.deleteFutarFromList(id);
                }
                catch (RepositoryExceptionCantDelete recd)
                {
                    kiirHibauzenetet(recd.Message);
                    Debug.WriteLine("A futár törlés nem sikerült, nincs a listába!");
                }
                //2. törölni kell az adatbázisból
                RepositoryDatabaseTablePizza rdtp = new RepositoryDatabaseTablePizza();
                try
                {
                    rdtp.deleteFutarFromDatabase(id);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. frissíteni kell a DataGridView-t  
                frissitFutarAdatokkalDataGriedViewt();
                if (dataGridViewFutars.SelectedRows.Count <= 0)
                {
                    buttonUjFutar.Visible = true;
                }
                bealliFutartPizzaDataGriViewt();
            }
        }
        private void buttonModositFutar_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            errorProviderFutarNev.Clear();
            errorProviderFutarLakcim.Clear();
            errorProviderFutarTelefonszam.Clear();
            errorProviderFutarEmail.Clear();
            try
            {
                Futar modosult = new Futar(
                    Convert.ToInt32(textBoxFutarAzon.Text),
                    textBoxFutarNAME.Text,
                    textBoxFutarADDRESS.Text,
                    textBoxFutarPHONENUM.Text,
                    textBoxFutarEMAIL.Text
                    );
                int azonosito = Convert.ToInt32(textBoxFutarAzon.Text);
                //1. módosítani a listába
                try
                {
                    futarRepo.updateFutarInList(azonosito, modosult);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                    return;
                }
                //2. módosítani az adatbáziba
                RepositoryDatabaseTablePizza rdtp = new RepositoryDatabaseTablePizza();
                try
                {
                    rdtp.updateFutarInDatabase(azonosito, modosult);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. módosítani a DataGridView-ban           
                frissitFutarAdatokkalDataGriedViewt();
            }
            catch (ModelFutarNotValidNameExeption nvn)
            {
                errorProviderFutarNev.SetError(textBoxFutarNAME, nvn.Message);
            }
            catch (ModelFutarNotValidLakcimExeption nvl)
            {
                errorProviderFutarLakcim.SetError(textBoxFutarADDRESS, nvl.Message);
            }
            catch (ModelFutarNotValidTelefonszamExeption nvt)
            {
                errorProviderFutarTelefonszam.SetError(textBoxFutarPHONENUM, nvt.Message);
            }
            catch (ModelFutarNotValidEmailExeption nve)
            {
                errorProviderFutarEmail.SetError(textBoxFutarEMAIL, nve.Message);
            }
            catch (RepositoryExceptionCantModified recm)
            {
                kiirHibauzenetet(recm.Message);
                Debug.WriteLine("Módosítás nem sikerült, a futár nincs a listába!");
            }
            catch (Exception ex)
            { }
        }

        private void buttonUjFutarMentes_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            errorProviderFutarNev.Clear();
            errorProviderFutarLakcim.Clear();
            errorProviderFutarTelefonszam.Clear();
            errorProviderFutarEmail.Clear();
            try
            {
                Futar ujFutar = new Futar(
                    Convert.ToInt32(textBoxFutarAzon.Text),
                    textBoxFutarNAME.Text,
                    textBoxFutarADDRESS.Text,
                    textBoxFutarPHONENUM.Text,
                    textBoxFutarEMAIL.Text
                    );
                int azonosito = Convert.ToInt32(textBoxFutarAzon.Text);
                //1. Hozzáadni a listához
                try
                {
                    futarRepo.addFutarToList(ujFutar);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                    return;
                }
                //2. Hozzáadni az adatbázishoz
                RepositoryDatabaseTablePizza rdtp = new RepositoryDatabaseTablePizza();
                try
                {
                    rdtp.insertFutarToDatabase(ujFutar);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. Frissíteni a DataGridView-t
                beallitGombokatUjFutarMegsemEsMentes();
                frissitFutarAdatokkalDataGriedViewt();
                if (dataGridViewFutars.SelectedRows.Count == 1)
                {
                    bealliFutartPizzaDataGriViewt();
                }

            }
            catch (ModelFutarNotValidNameExeption nvn)
            {
                errorProviderFutarNev.SetError(textBoxFutarNAME, nvn.Message);
            }
            catch (ModelFutarNotValidLakcimExeption nvl)
            {
                errorProviderFutarLakcim.SetError(textBoxFutarADDRESS, nvl.Message);
            }
            catch (ModelFutarNotValidTelefonszamExeption nvt)
            {
                errorProviderFutarTelefonszam.SetError(textBoxFutarPHONENUM, nvt.Message);
            }
            catch (ModelFutarNotValidEmailExeption nve)
            {
                errorProviderFutarEmail.SetError(textBoxFutarEMAIL, nve.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void buttonUjFutar_Click(object sender, EventArgs e)
        {
            ujFutarAdatfelvitel = true;
            beallitGombokatTextboxokatUjFutarnal();
            int ujFutarAzonosito = futarRepo.getNextFutarId();
            textBoxFutarAzon.Text = ujFutarAzonosito.ToString();
        }

        private void buttonFutarMegse_Click(object sender, EventArgs e)
        {
            beallitGombokatUjFutarMegsemEsMentes();
        }

        private void beallitGombokatUjFutarMegsemEsMentes()
        {
            if ((dataGridViewFutars.Rows != null) &&
                (dataGridViewFutars.Rows.Count > 0))
                dataGridViewFutars.Rows[0].Selected = true;
            buttonUjFutarMentes.Visible = false;
            buttonFutarMegse.Visible = false;
            panelModositTorol.Visible = true;
            ujFutarAdatfelvitel = false;

            textBoxFutarNAME.Text = string.Empty;
            textBoxFutarADDRESS.Text = string.Empty;
            textBoxFutarPHONENUM.Text = string.Empty;
            textBoxFutarEMAIL.Text = string.Empty;

        }
        private void beallitGombokatTextboxokatUjFutarnal()
        {
            panelFutar.Visible = true;
            panelModositTorol.Visible = false;
            textBoxFutarNAME.Text = string.Empty;
            textBoxFutarADDRESS.Text = string.Empty;
            textBoxFutarPHONENUM.Text = string.Empty;
            textBoxFutarEMAIL.Text = string.Empty;
        }

        private void beallitFutarGombokatKattintaskor()
        {
            ujFutarAdatfelvitel = false;
            buttonUjFutarMentes.Visible = false;
            buttonFutarMegse.Visible = false;
            panelModositTorol.Visible = true;
            //errorProviderPizzaName.Clear();
            //errorProviderPizzaPrice.Clear();
        }
        private void textBoxFutarNAME_TextChanged(object sender, EventArgs e)
        {
            kezelFutarUjMegsemGombokat();
        }
        private void textBoxFutarADDRESS_TextChanged(object sender, EventArgs e)
        {
            kezelFutarUjMegsemGombokat();
        }

        private void textBoxFutarPHONENUM_TextChanged(object sender, EventArgs e)
        {
            kezelFutarUjMegsemGombokat();
        }
        private void textBoxFutarEMAIL_TextChanged(object sender, EventArgs e)
        {
            kezelFutarUjMegsemGombokat();
        }

        private void kezelFutarUjMegsemGombokat()
        {
            if (ujFutarAdatfelvitel == false)
                return;
            if ((textBoxFutarNAME.Text != string.Empty) ||
                (textBoxFutarADDRESS.Text != string.Empty) ||
                (textBoxFutarPHONENUM.Text != string.Empty) ||
                (textBoxFutarEMAIL.Text != string.Empty))
            {
                buttonUjFutarMentes.Visible = true;
                buttonFutarMegse.Visible = true;
            }
            else
            {
                buttonUjFutarMentes.Visible = false;
                buttonFutarMegse.Visible = false;
            }
        }

    }
}
