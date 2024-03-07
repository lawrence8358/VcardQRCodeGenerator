using Newtonsoft.Json;
using Npoi.Mapper;
using System; 
using System.ComponentModel; 
using VcardQRCodeGenerator.Model;

namespace VcardQRCodeGenerator.Utility
{
    internal class ExcelObj
    {
        public List<CardModel> GetDataSource(string excelFilePath, string configValue)
        {
            var configs = JsonConvert.DeserializeObject<List<ConfigModel>>(configValue);
            if (configs == null) throw new Exception("參數設定有問題，請重新檢查是否正確");

            var result = new List<CardModel>();
            var mapper = new Mapper(excelFilePath);
            var excelRows = mapper.Take<dynamic>(sheetIndex: 0).ToList();

            // 迴圈執行 excelRows 的資料，但欄位必須在 config 中有對應的欄位才需要寫入
            foreach (var excelRow in excelRows)
            {
                foreach (var config in configs)
                {
                    var vcard = GetVCardContent(excelRow, config);
                    if (vcard != null) result.Add(vcard);
                }
            }

            return result;
        }

        /// <summary>
        /// 取得 VCard 內容
        /// </summary> 
        private CardModel? GetVCardContent(RowInfo<dynamic> excelRow, ConfigModel config)
        {
            Dictionary<string, string> excelItem = Dyn2Dict(excelRow.Value);

            var model = new CardModel { Lang = config.Lang };
            var filedContent = string.Empty;

            #region 取得欄位內容對應的 VCard 欄位

            foreach (var filed in config.Fields)
            {
                if (excelItem.ContainsKey(filed.Excel))
                {
                    var excelValue = excelItem[filed.Excel];
                    var vcardField = filed.VCard;

                    if (!string.IsNullOrEmpty(excelValue))
                    {
                        if (!string.IsNullOrEmpty(filedContent)) filedContent += Environment.NewLine;
                        filedContent += $"{vcardField}{excelValue}";
                    }

                    if (filed.Key) model.FileName = excelValue.ToString();
                }
            }

            #endregion

            if (!string.IsNullOrEmpty(filedContent))
            {
                model.Content = filedContent;
                return model;
            }

            return null;
        }

        private Dictionary<string, string> Dyn2Dict(dynamic dynObj)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dynObj))
            {
                var obj = propertyDescriptor.GetValue(dynObj) as string;
                if (obj != null) dictionary.Add(propertyDescriptor.Name, obj);
            }
            return dictionary;
        }
    }
}
