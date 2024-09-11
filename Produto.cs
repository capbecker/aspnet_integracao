namespace Backend.API

public class Produto
{
	private string nome;

	private double preco;

	public void setNome(string nome) { this.nome = nome; }

	public string getNome() { return nome; }

	public void setPreco(double preco) { this.preco = preco; }

	public double getPreco() { return preco;}
}
