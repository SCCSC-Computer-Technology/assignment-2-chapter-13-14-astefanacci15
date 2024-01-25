using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// My second attempt after Danielle's help --- ty Danielle!!!!!!!

namespace CPT_206_HW_Assignment1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'productDBDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.productDBDataSet.Product);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // the easiest button, so it is first
            this.Close();
        }

        private void buttonProductNumber_Click(object sender, EventArgs e)
        {
            // Assignment 4 
            // Clear Result List Box
            listBoxResults.Items.Clear();

            // Create variable for product number
            string productNumber = "";

            try
            {
                // To ensure user inputs something to calculate
                if(textBoxProductNumber.Text == "")
                {
                    MessageBox.Show("Please enter a Product Number to find");
                }
                else
                {
                    // Find the user input
                    productNumber = textBoxProductNumber.Text;
                    DataClassesProductDataContext db = new DataClassesProductDataContext();
                    var results = from Product in db.Products
                                  where Product.Product_Number == productNumber
                                  select Product.Description;

                    foreach (string description in results)
                    {
                        listBoxResults.Items.Add(description);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There is an error with 'Finding Product Number'");
            }
        }

        private void buttonProductDesc_Click(object sender, EventArgs e)
        {
            // Assignment 4 
            // Clear Result List Box
            listBoxResults.Items.Clear();

            // Create variable for product number
            string productDescription = "";

            try
            {
                // To ensure user inputs something to calculate
                if (textBoxProductNumber.Text == "")
                {
                    MessageBox.Show("Please enter a Product Description to find");
                }
                else
                {
                    // Find the user input
                    productDescription = textBoxProductDesc.Text;
                    DataClassesProductDataContext db = new DataClassesProductDataContext();
                    var results = from Product in db.Products
                                  where Product.Description == productDescription
                                  select Product.Description;

                    foreach (string description in results)
                    {
                        listBoxResults.Items.Add(description);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There is an error with 'Finding Product Description'");
            }
        }

        private void buttonQuantity_Click(object sender, EventArgs e)
        {
            // Assignment 5
            // Clear Result List Box
            listBoxResults.Items.Clear();

            // Create variable for Quantity
            int quantity = 0;
            try
            {
                // parse quantity variable to switch from text
                quantity = int.Parse(textBoxQuantity.Text);

                if (textBoxQuantity.Text == "")
                {
                    MessageBox.Show("Please enter a Quantity to calculate");
                }
                else
                {
                    DataClassesProductDataContext db = new DataClassesProductDataContext();
                    // If Max button is checked off
                    if (radioButtonMax.Checked)
                    {
                        var results = from product in db.Products
                                      where product.Units_On_Hand > quantity
                                      select product.Description;
                        foreach (string description in results)
                        {
                            listBoxResults.Items.Add(description);
                        }
                    }
                    // if Min button is checked off
                    else if (radioButtonMin.Checked)
                    {
                        var results = from product in db.Products
                                      where product.Units_On_Hand < quantity
                                      select product.Description;
                        foreach (string description in results)
                        {
                            listBoxResults.Items.Add(description);
                        }
                    }
                    // If NEITHER button is checked off, throw error message
                    else
                    {
                        MessageBox.Show("Please check off a choice of min/max");
                    }
                }
            }
            catch
            {
                MessageBox.Show("There is an error with 'Finding Quantity Min/Max'");
            }
        }

        private void buttonASC_Click(object sender, EventArgs e)
        {
            // Technically Assignment 4 - I just liked it better outside the group box

            // Clear Result List Box
            listBoxResults.Items.Clear();

            DataClassesProductDataContext db = new DataClassesProductDataContext();
            var results = from product in db.Products
                          orderby product.Units_On_Hand
                          select product.Description;
            foreach (string description in results)
            {
                listBoxResults.Items.Add(description);
            }
        }

        private void buttonPrice_Click(object sender, EventArgs e)
        {
            // Assignment 6

            // Clear Result List Box
            listBoxResults.Items.Clear();

            // create variable for price
            decimal price = 0.0m;
            try
            {
                // parse price to get from text
                price = decimal.Parse(textBoxPrice.Text);

                DataClassesProductDataContext db = new DataClassesProductDataContext();
                if (radioButtonMoreThan.Checked)
                {
                    var results = from product in db.Products
                                  where product.Price < price
                                  select product.Description;
                    foreach (string description in results)
                    {
                        listBoxResults.Items.Add(description);
                    }
                }
                else if (radioButtonLessThan.Checked)
                {
                    var results = from product in db.Products
                                  where product.Price > price
                                  select product.Description;
                    foreach (string description in results)
                    {
                        listBoxResults.Items.Add(description);
                    }
                }
                // throw error if neither more than or less than is checked off
                else
                {
                    MessageBox.Show("Please check off a choice of more than/less than");
                }
            }
            catch
            {
                MessageBox.Show("There is an error with 'Sorting Price'");
            }

        }
    }
}
