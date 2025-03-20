namespace FinancialAccounting
{
    // Класс для экспорта данных в строку
    public class Exporter
    {
        // Экспорт операций в строку (формат: "тип|сумма|дата|категория|описание")
        public string ExportOperations(List<Operation> operations, List<Category> categories)
        {
            var result = new List<string>();

            foreach (var operation in operations)
            {
                var category = categories.FirstOrDefault(c => c.Id == operation.CategoryId);
                if (category == null)
                    throw new ArgumentException($"Категория с ID {operation.CategoryId} не найдена.");

                var type = operation.Type == OperationType.Income ? "Доход" : "Расход";
                var line = $"{type}|{operation.Amount}|{operation.Date:yyyy-MM-dd}|{category.Name}|{operation.Description}";
                result.Add(line);
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}