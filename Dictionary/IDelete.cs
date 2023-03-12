using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public interface IDelete
    {
        void DeleteTranslation(string word, string translation);
        void DeleteWord(string word);
    }
}
