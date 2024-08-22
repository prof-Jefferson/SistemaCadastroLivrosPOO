using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SistemaCadastroLivrosPOO.Services
{
    public class GerenciadorDeArquivo<T>
    {
        private readonly string _caminhoArquivo;

        public GerenciadorDeArquivo(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;
        }

        public void Salvar(List<T> dados)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                File.WriteAllText(_caminhoArquivo, json);
                Console.WriteLine($"Dados salvos com sucesso no arquivo {_caminhoArquivo}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar os dados: {ex.Message}");
            }
        }

        public List<T> Carregar()
        {
            try
            {
                if (File.Exists(_caminhoArquivo))
                {
                    var json = File.ReadAllText(_caminhoArquivo);
                    var dados = JsonConvert.DeserializeObject<List<T>>(json);
                    Console.WriteLine($"Dados carregados com sucesso do arquivo {_caminhoArquivo}.");
                    return dados;
                }
                else
                {
                    Console.WriteLine($"Arquivo {_caminhoArquivo} não encontrado. Retornando uma lista vazia.");
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
