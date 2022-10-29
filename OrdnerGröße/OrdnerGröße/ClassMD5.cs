using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordner_;

namespace OrdnerGröße
{
    public class ClassMD5
    {
        public byte[] md5;
        public List<ClassDatei> duplikate = new List<ClassDatei>();
        
        public ClassMD5()
        {

        }

        public ClassMD5(List<ClassDatei> duplikate)
        {
            this.duplikate = duplikate;
        }

        public ClassMD5(byte[] md5, List<ClassDatei> duplikate)
        {
            this.md5 = md5;
            this.duplikate = duplikate;
        }

        public ClassMD5(ClassDatei cd)
        {
            md5 = null;
            duplikate = new List<ClassDatei>();
            duplikate.Add(cd);
        }

        public ClassMD5(ClassDatei cd, byte[] md5)
        {
            this.md5 = md5;
            duplikate = new List<ClassDatei>();
            duplikate.Add(cd);
        }

        public long GetDateiGröße()
        {
            if (duplikate.Count == 0) return 0;
            return duplikate.First().größe;
        }

        public long GetDuplikateGesamtGröße()
        {
            if (duplikate.Count == 0) return 0;

            return duplikate.First().größe * (duplikate.Count-1);
        }

    }
}
