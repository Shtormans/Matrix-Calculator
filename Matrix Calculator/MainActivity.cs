using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace Matrix_Calculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Matrix[] matrixes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //SetContentView(Resource.Layout.Main);  


            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.matrix_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Button button = FindViewById<Button>(Resource.Id.createMatrixButton);
            button.Click += OnCreateButtonClicked;

            matrixes = new Matrix[3] { new Matrix(), new Matrix(), new Matrix() };
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            GridLayout matrixGrid = FindViewById<GridLayout>(Resource.Id.matrixGrid);
            EditText matrixHeight = FindViewById<EditText>(Resource.Id.matrixHeight);
            EditText matrixWidth = FindViewById<EditText>(Resource.Id.matrixWidth);

            matrixGrid.RemoveAllViews();
            matrixHeight.Text = matrixes[spinner.SelectedItemPosition].rows.ToString();
            matrixWidth.Text = matrixes[spinner.SelectedItemPosition].columns.ToString();

            int height = Convert.ToInt32(matrixHeight.Text);
            int width = Convert.ToInt32(matrixWidth.Text);

            if (matrixes[spinner.SelectedItemPosition].matrixValues.Count == 0)
                return;

            for (int i = 0; i < Convert.ToInt32(matrixHeight.Text) * Convert.ToInt32(matrixWidth.Text); i++)
            {
                matrixGrid.AddView(new EditText(this)
                {
                    Text = matrixes[spinner.SelectedItemPosition].matrixValues[i].ToString(),
                    InputType = Android.Text.InputTypes.ClassNumber
                });
            }
        }
        private void OnCreateButtonClicked(object sender, System.EventArgs e)
        {
            GridLayout matrixGrid = FindViewById<GridLayout>(Resource.Id.matrixGrid);
            EditText matrixHeight = FindViewById<EditText>(Resource.Id.matrixHeight);
            EditText matrixWidth = FindViewById<EditText>(Resource.Id.matrixWidth);

            if (matrixHeight.Text == string.Empty)
            {
                matrixHeight.Text = "1";
            }
            if (matrixWidth.Text == string.Empty)
            {
                matrixWidth.Text = "1";
            }

            matrixGrid.RemoveAllViews();

            int height = Convert.ToInt32(matrixHeight.Text);
            int width = Convert.ToInt32(matrixWidth.Text);

            matrixGrid.RowCount = height;
            matrixGrid.ColumnCount = width;

            for (int i = 0; i < height * width; i++)
            {
                matrixGrid.AddView(new EditText(this)
                {
                    InputType = Android.Text.InputTypes.ClassNumber
                });
            }

            Button addMatrixButton = new Button(this)
            {
                Text = "Add",
                TextSize = 8
            };

            addMatrixButton.Click += OnAddButtonClicked;
            addMatrixButton.SetX(800);
            addMatrixButton.SetY(590);
            RelativeLayout content = FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
            content.AddView(addMatrixButton);
        }
        private void OnAddButtonClicked(object sender, System.EventArgs e)
        {
            GridLayout matrixGrid = FindViewById<GridLayout>(Resource.Id.matrixGrid);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            matrixes[spinner.SelectedItemPosition].rows = matrixGrid.RowCount;
            matrixes[spinner.SelectedItemPosition].columns = matrixGrid.ColumnCount;
            matrixes[spinner.SelectedItemPosition].matrixValues.Clear();
            for (int i = 0; i < matrixGrid.ChildCount; i++)
            {
                if (((EditText)matrixGrid.GetChildAt(i)).Text == string.Empty)
                    ((EditText)matrixGrid.GetChildAt(i)).Text = "0";

                matrixes[spinner.SelectedItemPosition].matrixValues.Add(Convert.ToInt32(((EditText)matrixGrid.GetChildAt(i)).Text));
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}