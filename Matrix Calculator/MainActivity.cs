using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Matrix_Calculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        enum components
        {
            number,
            matrix
        }

        Matrix[] matrixes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            GoToMainPage();
            matrixes = new Matrix[6] { new Matrix(), new Matrix(), new Matrix(), new Matrix(), new Matrix(), new Matrix() };
        }
        private void GoToMainPage()
        {
            SetContentView(Resource.Layout.activity_main);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.matrix_array, Android.Resource.Layout.SimpleSpinnerItem);
            spinner.Adapter = adapter;

            Button button = FindViewById<Button>(Resource.Id.createMatrixButton);
            button.Click += OnCreateButtonClicked;

            Button button2 = FindViewById<Button>(Resource.Id.calculateButton);
            button2.Click += OnCalculateButtonClicked;

            Button addMatrixButton = FindViewById<Button>(Resource.Id.addMatrixButton);
            addMatrixButton.Click += OnAddButtonClicked;
            addMatrixButton.Visibility = Android.Views.ViewStates.Invisible;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Button addMatrixButton = FindViewById<Button>(Resource.Id.addMatrixButton);
            GridLayout matrixGrid = FindViewById<GridLayout>(Resource.Id.matrixGrid);
            EditText matrixHeight = FindViewById<EditText>(Resource.Id.matrixHeight);
            EditText matrixWidth = FindViewById<EditText>(Resource.Id.matrixWidth);

            addMatrixButton.Visibility = Android.Views.ViewStates.Invisible;
            matrixGrid.RemoveAllViews();
            matrixHeight.Text = "";
            matrixWidth.Text = "";

            if (matrixes[spinner.SelectedItemPosition].matrixValues.Count == 0)
                return;

            matrixHeight.Text = matrixes[spinner.SelectedItemPosition].rows.ToString();
            matrixWidth.Text = matrixes[spinner.SelectedItemPosition].columns.ToString();

            int height = Convert.ToInt32(matrixHeight.Text);
            int width = Convert.ToInt32(matrixWidth.Text);

            matrixGrid.RemoveAllViews();
            matrixGrid.RowCount = height;
            matrixGrid.ColumnCount = width;

            for (int i = 0; i < Convert.ToInt32(matrixHeight.Text) * Convert.ToInt32(matrixWidth.Text); i++)
            {
                matrixGrid.AddView(new EditText(this)
                {
                    Text = matrixes[spinner.SelectedItemPosition].matrixValues[i].ToString(),
                    InputType = Android.Text.InputTypes.ClassNumber,
                    ImeOptions = Android.Views.InputMethods.ImeAction.Next
                });
            }

            addMatrixButton.Visibility = Android.Views.ViewStates.Visible;
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
                    InputType = Android.Text.InputTypes.ClassNumber,
                    ImeOptions = Android.Views.InputMethods.ImeAction.Next
                });
            }

            Button addMatrixButton = FindViewById<Button>(Resource.Id.addMatrixButton);
            if (addMatrixButton.Visibility == Android.Views.ViewStates.Invisible)
            {
                addMatrixButton.Visibility = Android.Views.ViewStates.Visible;
            }
        }
        private void OnAddButtonClicked(object sender, System.EventArgs e)
        {
            GridLayout matrixGrid = FindViewById<GridLayout>(Resource.Id.matrixGrid);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            matrixes[spinner.SelectedItemPosition].rows = matrixGrid.RowCount;
            matrixes[spinner.SelectedItemPosition].columns = matrixGrid.ColumnCount;
            matrixes[spinner.SelectedItemPosition].matrixValues.Clear();

            int selectedItemPos = spinner.SelectedItemPosition;

            for (int i = 0; i < matrixGrid.ChildCount; i++)
            {
                string matrixValue = ((EditText)matrixGrid.GetChildAt(i)).Text;

                if (matrixValue == string.Empty)
                    ((EditText)matrixGrid.GetChildAt(i)).Text = "0";

                matrixes[selectedItemPos].matrixValues.Add(Convert.ToInt32(matrixValue));
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void OnCalculateButtonClicked(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.calculate_page);

            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);

            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.matrix_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;

            Button button;
            button = FindViewById<Button>(Resource.Id.buttonNumber0);
            button.Click += OnButtonNumber0;
            button = FindViewById<Button>(Resource.Id.buttonNumber1);
            button.Click += OnButtonNumber1;
            button = FindViewById<Button>(Resource.Id.buttonNumber2);
            button.Click += OnButtonNumber2;
            button = FindViewById<Button>(Resource.Id.buttonNumber3);
            button.Click += OnButtonNumber3;
            button = FindViewById<Button>(Resource.Id.buttonNumber4);
            button.Click += OnButtonNumber4;
            button = FindViewById<Button>(Resource.Id.buttonNumber5);
            button.Click += OnButtonNumber5;
            button = FindViewById<Button>(Resource.Id.buttonNumber6);
            button.Click += OnButtonNumber6;
            button = FindViewById<Button>(Resource.Id.buttonNumber7);
            button.Click += OnButtonNumber7;
            button = FindViewById<Button>(Resource.Id.buttonNumber8);
            button.Click += OnButtonNumber8;
            button = FindViewById<Button>(Resource.Id.buttonNumber9);
            button.Click += OnButtonNumber9;

            button = FindViewById<Button>(Resource.Id.buttonClear);
            button.Click += OnButtonClear;
            button = FindViewById<Button>(Resource.Id.buttonClearCell);
            button.Click += OnButtonClearCell;
            button = FindViewById<Button>(Resource.Id.buttonEqual);
            button.Click += OnButtonEqual;

            button = FindViewById<Button>(Resource.Id.buttonDivide);
            button.Click += OnButtonDivide;
            button = FindViewById<Button>(Resource.Id.buttonMultiply);
            button.Click += OnButtonMultiply;
            button = FindViewById<Button>(Resource.Id.buttonAdd);
            button.Click += OnButtonAdd;
            button = FindViewById<Button>(Resource.Id.buttonSubtract);
            button.Click += OnButtonSubtract;

            button = FindViewById<Button>(Resource.Id.buttonA);
            button.Click += OnButtonA;

            button = FindViewById<Button>(Resource.Id.buttonB);
            button.Click += OnButtonB;

            button = FindViewById<Button>(Resource.Id.buttonC);
            button.Click += OnButtonC;

            button = FindViewById<Button>(Resource.Id.buttonExit);
            button.Click += OnButtonExit;
        }
        private void OnButtonExit(object sender, System.EventArgs e)
        {
            GoToMainPage();
        }
        private int MakeAction(int num1, char action, int num2)
        {
            switch (action)
            {
                case '/':
                    num1 /= num2;
                    break;
                case 'x':
                    num1 *= num2;
                    break;
                case '+':
                    num1 += num2;
                    break;
                case '-':
                    num1 -= num2;
                    break;
            }

            return num1;
        }
        private Matrix MatrixDivide(Matrix matrix1, Matrix matrix2)
        {
            return matrix1;
        }
        private Matrix MatrixMultiply(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix = new Matrix();
            matrix.rows = matrix1.rows;
            matrix.columns = matrix2.columns;

            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix1.columns; j++)
                {
                    int index = i * matrix1.columns + j;

                    int value = 0;
                    for (int k = 0; k < matrix1.columns; k++)
                    {
                        int index1 = i * matrix1.columns + k;
                        int index2 = k * matrix1.columns + j;

                        value += matrix1.matrixValues[index1] * matrix2.matrixValues[index2];
                    }

                    matrix.matrixValues.Add(value);
                }
            }

            return matrix;
        }
        private Matrix MatrixAdd(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix = new Matrix();
            matrix.rows = matrix1.rows;
            matrix.columns = matrix2.columns;

            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix1.columns; j++)
                {
                    int index = i * matrix1.columns + j;
                    int value = matrix1.matrixValues[index] + matrix2.matrixValues[index];
                    matrix.matrixValues.Add(value);
                }
            }

            return matrix;
        }
        private Matrix MatrixSubstract(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix = new Matrix();
            matrix.rows = matrix1.rows;
            matrix.columns = matrix2.columns;

            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix1.columns; j++)
                {
                    int index = i * matrix1.columns + j;
                    int value = matrix1.matrixValues[index] - matrix2.matrixValues[index];
                    matrix.matrixValues.Add(value);
                }
            }

            return matrix;
        }
        private Matrix MakeAction(Matrix matrix1, char action, Matrix matrix2)
        {
            Matrix matrix = new Matrix();

            switch (action)
            {
                case '/':
                    matrix = MatrixDivide(matrix1, matrix2);
                    break;
                case 'x':
                    matrix = MatrixMultiply(matrix1, matrix2);
                    break;
                case '+':
                    matrix = MatrixAdd(matrix1, matrix2);
                    break;
                case '-':
                    matrix = MatrixSubstract(matrix1, matrix2);
                    break;
            }

            return matrix;
        }
        private Matrix MakeIdentityMatrix(int number, Matrix matrix)
        {
            Matrix identityMatrix = new Matrix();
            identityMatrix.rows = matrix.rows;
            identityMatrix.columns = matrix.columns;

            for (int i = 0; i < identityMatrix.rows; i++)
            {
                for (int j = 0; j < identityMatrix.columns; j++)
                {
                    if (i == j)
                        identityMatrix.matrixValues.Add(number);
                    else
                        identityMatrix.matrixValues.Add(0);
                }
            }
            return identityMatrix;
        }
        private int GetComponents(out components comp1, out char action, out components comp2, string text)
        {
            action = '+';

            char firstChar = text[0];
            comp1 = char.IsDigit(firstChar) ? components.number : components.matrix;

            char lastChar = text[text.Length - 1];
            comp2 = char.IsDigit(lastChar) ? components.number : components.matrix;

            for (int i = 1; i < text.Length - 1; i++)
            {
                char character = text[i];
                if ("/x+-".Contains(character))
                {
                    action = character;
                    return i;
                }
            }
            return -1;
        }
        private Matrix CalculateResult(TextView textView)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner2);
            TextView textView2 = FindViewById<TextView>(Resource.Id.textView2);
            Matrix matrix = new Matrix();
            string text = textView.Text;
            components comp1, comp2;
            char action;
            int index = GetComponents(out comp1, out action, out comp2, text);
            if (index == -1)
            {
                if (comp1 == components.number)
                {
                    int value = Convert.ToInt32(text);

                    matrix.rows = 1;
                    matrix.columns = 1;
                    matrix.matrixValues.Add(value);
                }
                else
                {
                    int matrixChar1 = text[0];
                    int firstIndex = (int)'A';

                    int matrixIndex1 = matrixChar1 - firstIndex;

                    Matrix matrix1 = matrixes[matrixIndex1];

                    matrix.rows = matrix1.rows;
                    matrix.columns = matrix1.columns;
                    matrix.matrixValues = new List<Int32>(matrix1.matrixValues);

                    textView.Text = "";
                }
            }
            else if (comp1 != comp2)
            {
                if (comp1 == components.number)
                {
                    int num1 = Convert.ToInt32(text.Substring(0, index));
                    int matrixChar2 = text[2];
                    int firstIndex = (int)'A';

                    int matrixIndex2 = matrixChar2 - firstIndex;

                    Matrix matrix2 = matrixes[matrixIndex2];
                    Matrix matrix1 = MakeIdentityMatrix(num1, matrix2);

                    matrix = MakeAction(matrix1, action, matrix2);

                    textView.Text = "";
                }
                else
                {
                    int matrixChar1 = text[0];
                    int num2 = Convert.ToInt32(text.Substring(index + 1, text.Length - index - 1));
                    int firstIndex = (int)'A';

                    int matrixIndex1 = matrixChar1 - firstIndex;

                    Matrix matrix1 = matrixes[matrixIndex1];
                    Matrix matrix2 = MakeIdentityMatrix(num2, matrix1);

                    matrix = MakeAction(matrix1, action, matrix2);

                    textView.Text = "";
                }
            }
            else if (comp1 == components.number)
            {
                int num1 = Convert.ToInt32(text.Substring(0, index));
                int num2 = Convert.ToInt32(text.Substring(index + 1, text.Length - index - 1));
                int value = MakeAction(num1, action, num2);

                matrix.rows = 1;
                matrix.columns = 1;
                matrix.matrixValues.Add(value);

                textView.Text = value.ToString();
            }
            else if (comp1 == components.matrix)
            {
                int matrixChar1 = text[0];
                int matrixChar2 = text[2];
                int firstIndex = (int)'A';

                int matrixIndex1 = matrixChar1 - firstIndex;
                int matrixIndex2 = matrixChar2 - firstIndex;

                Matrix matrix1 = matrixes[matrixIndex1];
                Matrix matrix2 = matrixes[matrixIndex2];

                matrix = MakeAction(matrix1, action, matrix2);

                textView.Text = "";
            }

            textView2.Text = spinner.SelectedItem.ToString() + " has been updated";
            return matrix;
        }
        private string deleteLastCharacter(string text)
        {
            if (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }
        private string CheckTextAction(string text, char character)
        {
            if (text.Length == 0)
            {
                return text;
            }
            else
            {
                foreach (var item in text)
                {
                    if (item == '/' || item == 'x' || item == '+' || item == '-')
                    {
                        return text;
                    }
                }
            }
            if (text.Length > 0)
            {
                char lastCharacter = text[text.Length - 1];
                if (lastCharacter == '/' || lastCharacter == 'x' || lastCharacter == '+' || lastCharacter == '-')
                {
                    text = deleteLastCharacter(text);
                }
            }
            return text + character;
        }
        private string CheckTextMatrix(string text, char character)
        {
            if (text.Length > 0)
            {
                char lastCharacter = text[text.Length - 1];
                if (char.IsDigit(lastCharacter))
                {
                    if (text == CheckTextAction(text, 'x'))
                    {
                        return text;
                    }
                    text += 'x';
                }
                else if (lastCharacter == 'A' || lastCharacter == 'B' || lastCharacter == 'C')
                {
                    text = deleteLastCharacter(text);
                }
            }
            return text + character;
        }
        private string CheckTextNumber(string text, char character)
        {
            if (text.Length > 0)
            {
                char lastCharacter = text[text.Length - 1];
                if (!(char.IsDigit(lastCharacter) || lastCharacter == '/' || lastCharacter == 'x' || lastCharacter == '+' || lastCharacter == '-'))
                {
                    if (text == CheckTextAction(text, 'x'))
                    {
                        return text;
                    }
                    text += 'x';
                }
            }
            return text + character;
        }

        private void OnButtonNumber0(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '0');
            textView.Text = text;
        }
        private void OnButtonNumber1(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '1');
            textView.Text = text;
        }
        private void OnButtonNumber2(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '2');
            textView.Text = text;
        }
        private void OnButtonNumber3(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '3');
            textView.Text = text;
        }
        private void OnButtonNumber4(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '4');
            textView.Text = text;
        }
        private void OnButtonNumber5(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '5');
            textView.Text = text;
        }
        private void OnButtonNumber6(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '6');
            textView.Text = text;
        }
        private void OnButtonNumber7(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '7');
            textView.Text = text;
        }
        private void OnButtonNumber8(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '8');
            textView.Text = text;
        }
        private void OnButtonNumber9(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextNumber(text, '9');
            textView.Text = text;
        }
        private void OnButtonClear(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            textView.Text = string.Empty;
        }
        private void OnButtonClearCell(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            textView.Text = deleteLastCharacter(text);
        }
        private void OnButtonDivide(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextAction(text, '/');
            textView.Text = text;
        }
        private void OnButtonMultiply(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextAction(text, 'x');
            textView.Text = text;
        }
        private void OnButtonAdd(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextAction(text, '+');
            textView.Text = text;
        }
        private void OnButtonSubtract(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextAction(text, '-');
            textView.Text = text;
        }
        private void OnButtonA(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextMatrix(text, 'A');
            textView.Text = text;
        }
        private void OnButtonB(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextMatrix(text, 'B');
            textView.Text = text;
        }
        private void OnButtonC(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            string text = textView.Text;
            text = CheckTextMatrix(text, 'C');
            textView.Text = text;
        }
        private void OnButtonEqual(object sender, System.EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.calculatorField);
            Matrix matrix = CalculateResult(textView);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner2);
            matrixes[spinner.SelectedItemPosition].rows = matrix.rows;
            matrixes[spinner.SelectedItemPosition].columns = matrix.columns;
            matrixes[spinner.SelectedItemPosition].matrixValues = new List<Int32>(matrix.matrixValues);
        }
    }
}