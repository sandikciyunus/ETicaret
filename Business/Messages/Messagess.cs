using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Messages
{
   public static class Messagess
    {
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";

        public static string CategoryAdded = "Kategori başarıyla eklendi";
        public static string CategoryDeleted = "Kategori başarıyla silindi";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi";

        public static string CategoryNotDeleted = "Bu kategori bir üründe kayıtlı silinemez";
    }
}
