namespace App
{
    public partial class Aluno
    {
        public override string ToString()
        {
            return string.Format("{0}", Nome);
        }
    }

    public partial class Nota
    {
        public override string ToString()
        {
            return string.Format("{0} -> {1}: {2}", Materia, Valor, Aluno);
        }
    }
}