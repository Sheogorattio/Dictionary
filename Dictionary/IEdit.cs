using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public interface IEdit
    {
        void EditWord(string word, string newWord);
        void EditTranslation(string word, string translation, string newTranslation);
    }
}
