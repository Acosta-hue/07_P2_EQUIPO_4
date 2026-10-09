using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("=== Simulación de ciclo de brazo robótico ===");

// Entradas del usuario
double tomar = Entrada.LeerDouble("Tiempo de la etapa TOMAR (s): ", 0, false);
double trasladar = Entrada.LeerDouble("Tiempo de la etapa TRASLADAR (s): ", 0, false);
double soltar = Entrada.LeerDouble("Tiempo de la etapa SOLTAR (s): ", 0, false);
int ciclos = Entrada.LeerEntero("Número de ciclos a simular (1 a 100000): ", 1, 100000);

// Objeto: brazo creado a partir de la clase BrazoRobotico
BrazoRobotico brazo = new BrazoRobotico(tomar, trasladar, soltar);

Console.WriteLine($"\nTiempo de un ciclo: {brazo.TiempoCiclo():F2} s\n");

// Ciclo for: muestra el tiempo acumulado tras cada repetición
double acumulado = 0;
for (int i = 1; i <= ciclos; i++)
{
    acumulado = brazo.TiempoAcumulado(i);
    Console.WriteLine($"Ciclo {i}: tiempo acumulado = {acumulado:F2} s");
}

Console.WriteLine($"\nTiempo total de producción: {acumulado:F2} s");

// ===== Clase: BrazoRobotico =====
class BrazoRobotico
{
    // Propiedades (double, en segundos)
    public double TiempoTomar { get; }
    public double TiempoTrasladar { get; }
    public double TiempoSoltar { get; }

    public BrazoRobotico(double tomar, double trasladar, double soltar)
    {
        TiempoTomar = tomar;
        TiempoTrasladar = trasladar;
        TiempoSoltar = soltar;
    }

    // Método: tiempo de un ciclo completo (s)
    public double TiempoCiclo() => TiempoTomar + TiempoTrasladar + TiempoSoltar;

    // Método: tiempo acumulado después de n ciclos (s)
    public double TiempoAcumulado(int n) => TiempoCiclo() * n;
}

// ===== Clase auxiliar: lectura segura de datos =====
static class Entrada
{
    // Lee un número decimal; repite mientras el dato no sea válido o no cumpla el mínimo
    public static double LeerDouble(string mensaje, double minimo, bool estricto)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? texto = Console.ReadLine();
            if (double.TryParse((texto ?? "").Trim().Replace(',', '.'),
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out double v)
                && (estricto ? v > minimo : v >= minimo))
                return v;
            Console.WriteLine(estricto ? $"  Valor inválido: debe ser un número mayor que {minimo}."
                                       : $"  Valor inválido: debe ser un número mayor o igual que {minimo}.");
        }
    }

    // Lee un entero dentro de un rango; repite mientras sea inválido
    public static int LeerEntero(string mensaje, int minimo, int maximo)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int v) && v >= minimo && v <= maximo)
                return v;
            Console.WriteLine($"  Valor inválido: debe ser un entero entre {minimo} y {maximo}.");
        }
    }

    // Lee texto no vacío
    public static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? t = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(t)) return t;
            Console.WriteLine("  El texto no puede estar vacío.");

        }
    }
}
