/**
 * This program writes a fully-functioning GUI program with C# and Windows Presentation Foundation.
 * It calculate the Pneumonia Severity Index (PSI) for patients in the Emergency Department who may have Community-Acquired Pneumonia (CAP).
 * It allows the user to enter the data and calculate a risk class with an admission recommendation.
 * @author Elena Milan Lopez
 * @version 1.0
 * Programming Project 2 - Solution
 * SP20
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSI_EML
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow : Window
     {
          public MainWindow()
          {
               InitializeComponent();
               //createFile();
          }

          private void score_Button_Click(object sender, RoutedEventArgs e)
          {
               //parse the text entered on each textbox to an int.

               int age = int.Parse(Age_TB.Text);
               int respiratory_rate = int.Parse(respiratoryR_TB.Text);
               int syst_blood_pressure = int.Parse(SBP_TB.Text);
               int temp = int.Parse(Temp_TB.Text);
               int pulse = int.Parse(Pulse_TB.Text);
               int pH = int.Parse(pH_TB.Text);
               int bun = int.Parse(BUN_TB.Text);
               int sodium = int.Parse(Sodium_TB.Text);
               int glucose = int.Parse(Glucose_TB.Text);
               int hematocrit = int.Parse(Hema_TB.Text);
               double partial_pressure = double.Parse(PartialPO_TB.Text);

               //Add the patient’s age to the score

               int score = age;

               int nursingHR = 0;
               int neoplasticD = 0;
               int liverD = 0;
               int congestiveHF = 0;
               int cerebrovD = 0;
               int renalD = 0;
               int alteredMS = 0;
               int pleuralE = 0;

               string gender = "Male";

               //If the patient is female, subtract 10 points

               if (Sex_Female_RDO.IsChecked == true)
               {
                    score -= 10;
                    gender = "Female";
               }

               //If the patient is a Nursing Home Resident, add 10 points.

               if (nursingHR_Y.IsChecked == true) {
                    score += 10;
                    nursingHR = 1;
               }

               //If the patient has Neoplastic Disease, add 30 points.

               if (neoplasticD_Y.IsChecked == true)
               {
                    score += 30;
                    neoplasticD = 1;
               }

               //If the patient has Liver Disease, add 20 points.

               if (liverD_Y.IsChecked == true)
               {
                    score += 20;
                    liverD = 1;
               }

               //If the patient has Congestive Heart Failure , add 10 points.

               if (congestiveHF_Y.IsChecked == true)
               {
                    score += 10;
                    congestiveHF = 1;
               }

               //If the patient has Cerebrovascular Disease , add 10 points.

               if (cerebrovD_Y.IsChecked == true)
               {
                    score += 10;
                    cerebrovD = 1;
               }

               //If the patient has Renal Disease , add 10 points.

               if (renalD_Y.IsChecked == true)
               {
                    score += 10;
                    renalD = 1;
               }

               //If the patient has Altered Mental Status , add 20 points.

               if (alteredMS_Y.IsChecked == true)
               {
                    score += 20;
                    alteredMS = 1;
               }

               //If the patient has respiratory rate ≥30 breaths/minute , add 20 points.

               if (respiratory_rate >= 30)
                    score += 20;

               //If the patient has systolic blood pressure <90mmHG , add 20 points.

               if (syst_blood_pressure < 90)
                    score += 20;

               //If the patient has temperature <35°C or > 39.9°, add 15 points.

               if (temp_celcius_RDO.IsChecked == true)
                    if (temp < 35 || temp > 39.9)
                         score += 15;

               //If the patient has temperature <95°F or >103.8°F, add 15 points and convert fahrenheit to celcius.

               if (temp_farengheit_RDO.IsChecked == true)
               {
                    if (temp < 95 || temp > 103.8)
                    {
                         score += 15;
                    }

                    temp = FTOC(temp);
               }

               //If the patient has a  pulse ≥ 125 beats/minute , add 10 points.

               if (pulse >= 125)
                    score += 10;

               //If the patient has a pH < 7.35 , add 30 points.

               if (pH < 7.35)
                    score += 30;

               //If the patient has BUN ≥30 mg/dL, add 20 points.

               if (BUN_mgDL_RDO.IsChecked == true)
                    if (bun >= 30)
                         score += 20;

               //If the patient has BUN ≥11 mmol/L, add 20 points and convert mmol/L to mg/dL.

               if (BUN_mmolL_RDO.IsChecked == true) {
                    if (bun >= 11) {
                         score += 20;
                    }
                    bun = mmollTOmgdl(bun);
               }

               //If the patient has sodium < 130 mmol/L, add 20 points.

               if (sodium < 130)
                    score += 20;

               //If the patient has glucose ≥ 250 mg/dL, add 10 points.

               if (glucose_mgDL_RDO.IsChecked == true)
                    if (glucose >= 250)
                         score += 10;

               //If the patient has glucose ≥ 14 mmol/L, add 10 points and convert mmol/L to mg/dL.

               if (glucose_mmolL_RDO.IsChecked == true)
               {
                    if (glucose >= 14)
                    {
                         score += 10;
                    }
                    glucose = mmollTOmgdl(glucose);
               }

               //If the patient has hematocrit < 30%, add 10 points.

               if (hematocrit < 30)
                    score += 10;

               //If the patient has partial pressure of oxygen < 60 mmHg, add 10 points.

               if (partialP_mmHg_RDO.IsChecked == true)
                    if (partial_pressure < 60)
                         score += 10;

               //If the patient has partial pressure of oxygen < 8 kPa, add 10 points and convert kPa to mmHg.

               if (partialP_kPa_RDO.IsChecked == true)
               {
                    if (partial_pressure < 8)
                    {
                         score += 10;
                    }

                    partial_pressure = kPaTOmmHg(partial_pressure);
               }

               //If the patient has Pleural Effusion on x-ray , add 10 points.

               if (pleuralE_Y.IsChecked == true)
               {
                    score += 10;
                    pleuralE = 1;
               }

               //If the patient has no points from anywhere but age, they are assigned to Risk Class I.
               //Or if the patient is a female with no points from anywhere but age (then score will be equal to age - 10), they are assigned to Risk Class I.

               if (score == age || score == age -10)
                    score = 1;
            
               //Display message box. 

               admissionStatus(score);

               StreamWriter sw;
               int id;
               
               if (!File.Exists("./data.cvs"))
                    id = 0;
               else
               {
                    //Every time you write a new line to the file, it will get the next available id.

                    //This array of strings will contain all the lines of the file data.csv.
                    string[] data = File.ReadAllLines("./data.cvs");

                    //This array of strings contains the last line of data.csv separeted by commas. 
                    string[] line = data[data.Length - 1].Split(',');

                    //First position of array line contains the last id that was added to the file. 
                    id = int.Parse(line[0]);

               }//else

               //Append the variables to a text file named data.csv.
               //This file will contain one row for each patient evaluated.

               using (sw = File.AppendText("./data.cvs"))
               {                                          
                    sw.WriteLine((id + 1) + "," + age + "," + gender + "," + nursingHR + "," + neoplasticD + ","+ liverD + ","+
                         congestiveHF + ","+ cerebrovD + ","+ renalD + ","+ alteredMS + "," + pleuralE + "," +
                         respiratory_rate + "," + syst_blood_pressure + "," + temp + "," + pulse + "," +
                         pH + "," + bun + "," + sodium + "," + glucose + "," + hematocrit + "," + partial_pressure);
                    
                    sw.Close();
                   
               }//using 
               
          }//score_Button_Click()

          /*
           * This method will display a  message box indicating the risk class and admission recommendation.
           * 
           */
          public void admissionStatus(int score)
          {
               if (score == 1)
                    MessageBox.Show("Risk Class: I " + "\n" + "Risk: Low " + "\n" + "Admission Status: Outpatient Care");
               else if (score <= 70 && score != 1)
                    MessageBox.Show("Risk Class: II " + "\n" + "Risk: Low " + "\n" + "Admission Status: Outpatient Care");
               else if (score >= 71 && score <= 90)
                    MessageBox.Show("Risk Class: III " + "\n" + "Risk: Low " + "\n" + "Admission Status: Outpatient or Obervation Admission");
               else if (score >= 91 && score <= 130)
                    MessageBox.Show("Risk Class: IV " + "\n" + "Risk: Moderate " + "\n" + "Admission Status: Inpatient Admission");
               else
                    MessageBox.Show("Risk Class: V " + "\n" + "Risk: High " + "\n" + "Admission Status: Inpatient Admission (check for sepsis)");
          }       

          /*
           * This method will clear all the information entered so that the user can enter new data.
           */

          private void Clear_Click(object sender, RoutedEventArgs e)
          {
               foreach (Control ctl in Grid1.Children)
               {
                    //Clear all the checkboxes.

                    if (ctl.GetType() == typeof(CheckBox))
                         ((CheckBox)ctl).IsChecked = false;

                    //Clear all the textboxes.

                    if (ctl.GetType() == typeof(TextBox))
                         ((TextBox)ctl).Text = String.Empty;

                    //Clear all the radio buttons.

                    if (ctl.GetType() == typeof(RadioButton))
                         ((RadioButton)ctl).IsChecked = false;
                        
               }
          
          }

          /*
           * This method will convert the temperature from fahrenheit to celcius. 
           */

          private int FTOC(int temp)
          {
               int celsius = (temp - 32) * 5 / 9;
               return celsius;

          }//FTOC()

          /*
           * This method will convert the glucose or the BUN from  mmol/L to  mg/dL.
           */
          private int mmollTOmgdl (int mmoll)
          {
               int mgdl = 18 * mmoll;
               return mgdl;

          }//mgdlTOmmoll()

          /*
           * This method will convert the Partial pressure of oxygen from kPa to mmHg
           */
          private double kPaTOmmHg(double kPa)
          {
               double mmHg = 7.501 * kPa;
               return mmHg;

          }//kPaTOmmHg()

     }//MainWindow

}//namespace PSI_EML


 