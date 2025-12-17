# Grafo_2022-2

Trabalho de Grafos do 2º semestre de 2022.

Utilizado como método didático.

# Gerenciador de Grafos (Refatorado 2025)

Este projeto é uma implementação de uma estrutura de dados de Grafo em C#, desenvolvida originalmente em 2022 como um trabalho acadêmico. Em 2025, o projeto passou por uma refatoração completa para demonstrar evolução profissional, aplicação de Clean Code e otimização de algoritmos.

A atualização deste sistema legado foi realizada com o auxílio de ferramentas de Inteligência Artificial, garantindo a preservação de todas as funcionalidades originais enquanto modernizava a arquitetura e os processos de desenvolvimento. Esta abordagem exemplifica o uso de tecnologias atuais para impulsionar a melhoria contínua e a eficiência na manutenção de software.

O código original foi preservado em comentários para fins de comparação histórica, enquanto a nova implementação utiliza estruturas de dados modernas e eficientes.

## Funcionalidades

O sistema permite a manipulação de grafos não direcionados e ponderados, oferecendo as seguintes operações:

*   **Manipulação de Grafos:**
    *   Gerar Grafo Aleatório (para testes rápidos).
    *   Adicionar e remover vértices.
    *   Adicionar e remover arestas (com peso).
    *   Verificar adjacência entre vértices.
    *   Verificar se o grafo é completo.
    *   Verificar se o grafo é totalmente desconexo.
    *   Verificar se o grafo é Euleriano (Ciclo Euleriano).
    *   Exibir o grafo (Lista de Adjacência).

*   **Algoritmos:**
    *   **Dijkstra:** Encontra o menor caminho entre dois vértices (lógica corrigida para acumular custos).
    *   **Coloração de Grafos:** Implementação da heurística de **Welsh-Powell** (Guloso com ordenação de grau) e opção de "Pior Caso".
    *   **Detecção de Ciclos:**
        *   **Tarjan:** Algoritmo otimizado para encontrar Componentes Fortemente Conexos (SCCs).
        *   **Naive (Força Bruta):** Busca em profundidade (DFS) simples para fins didáticos.

*   **Persistência:**
    *   Salvar o grafo em arquivo CSV (caminho dinâmico na pasta "Documentos" do usuário).

## Tecnologias Utilizadas

*   C#
*   .NET Core 3.1 (Compatível com versões mais recentes)

## Melhorias Implementadas (Refatoração 2025)

Esta revisão técnica focou nos seguintes pontos:

1.  **Arquitetura e Código Limpo:**
    *   **Performance:** Substituição de `List<vertice>` por `Dictionary<string, Vertex>`, reduzindo a complexidade de busca de O(n) para O(1).
    *   **Padronização:** Adoção de convenções de nomenclatura do C# (PascalCase) e tradução interna das classes para inglês (`Graph`, `Vertex`, `Edge`).
    *   **SRP (Single Responsibility Principle):** Separação clara de responsabilidades. Cada algoritmo agora tem sua própria classe.
    *   Separação clara de responsabilidades (Classes dedicadas para algoritmos).

2.  **Correção de Bugs:**
    *   **Dijkstra:** A implementação original não acumulava o custo do caminho. A nova versão implementa o algoritmo corretamente.
    *   **Persistência:** O salvamento de arquivos agora utiliza `Environment.SpecialFolder.MyDocuments`, funcionando em qualquer computador (antes o caminho era fixo no C:).
    *   **Interface:** Menu reorganizado e protegido contra entradas inválidas.

3.  **Otimização:**
    *   Implementação da heurística de Welsh-Powell para coloração, garantindo uso mais eficiente de cores.
    *   Implementação do algoritmo de Tarjan para detecção de ciclos com complexidade linear.

## Como Executar

1.  Certifique-se de ter o .NET SDK instalado.
2.  Clone o repositório.
3.  Navegue até a pasta do projeto.
4.  Execute o comando:
    ```bash
    dotnet run
    ```

## Estrutura do Projeto

*   Programa.cs: Ponto de entrada e menu interativo.
*   Grafo.cs: Estrutura de dados principal (Vértice, Aresta, Grafo).
*   Algoritmo_D.cs: Implementação do algoritmo de Dijkstra.
*   Colorir_Grafo.cs: Implementação do algoritmo de coloração (Welsh-Powell).
*   Cycle.cs: Algoritmos de detecção de ciclos (Tarjan e Naive).
*   Save.cs: Responsável por exportar os dados para CSV na pasta do usuário.

---
*Projeto original de 2022. Refatoração concluída em Dezembro de 2025.*
