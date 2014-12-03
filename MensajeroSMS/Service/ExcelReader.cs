using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MensajeroSMS.Model;
using NPOI.SS.UserModel;

namespace MensajeroSMS.Service
{
    internal class ExcelReader
    {
        private readonly IWorkbook workBook;

        public ExcelReader(String fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            workBook = WorkbookFactory.Create(stream);
            stream.Close();
        }

        public List<Contacto> readData()
        {
            var contactos = new List<Contacto>();

            Contacto contact;

            IRow currentRow;
            ICell cell;

            ISheet sheet = workBook.GetSheetAt(0);

            IEnumerator rowEnumator = sheet.GetRowEnumerator();

            //Ignore headers
            rowEnumator.MoveNext();

            String name, cellphone;

            while (rowEnumator.MoveNext())
            {
                currentRow = rowEnumator.Current as IRow;

                cell = currentRow.GetCell(0, MissingCellPolicy.RETURN_BLANK_AS_NULL);
                name = cell.StringCellValue;

                cell = currentRow.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL);
                if (cell.CellType.CompareTo(CellType.Numeric) == 0)
                {
                    cellphone = cell.NumericCellValue.ToString();
                }
                else
                {
                    cellphone = cell.StringCellValue;
                }

                contact = new Contacto(name, cellphone);
                contactos.Add(contact);
            }

            return contactos;
        }
    }
}