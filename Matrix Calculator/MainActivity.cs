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
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }
        private void OnCreateButtonClicked(object sender, System.EventArgs e)
        {
            GridLayout matrixGrid = FindViewById<GridLayout>(Resource.Id.matrixGrid);
            int matrixHeight = Convert.ToInt32(FindViewById<EditText>(Resource.Id.matrixHeight).Text);
            int matrixWidth = Convert.ToInt32(FindViewById<EditText>(Resource.Id.matrixWidth).Text);

            matrixGrid.RowCount = matrixHeight;
            matrixGrid.ColumnCount = matrixWidth;
            for (int i = 0; i < matrixHeight*matrixHeight; i++)
            {
                matrixGrid.AddView(new EditText(this));
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

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}