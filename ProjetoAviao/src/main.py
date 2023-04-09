# start_imports
from random import Random
from time import time
from math import cos
from math import pi
from inspyred import ec
from inspyred.ec import terminators
import numpy as np
import os

import collections
collections.Iterable = collections.abc.Iterable
collections.Sequence = collections.abc.Sequence
# end_imports

# funcao que gera populacao


def generate_(random, args):
    size = args.get('num_inputs', 12)
    p = [0] * size
    # carga 1
    p[0] = random.randint(0, 16000)
    p[1] = random.randint(0, 16000)
    p[2] = random.randint(0, 16000)

    # carga 2
    p[3] = random.randint(0, 15000)
    p[4] = random.randint(0, 15000)
    p[5] = random.randint(0, 15000)

    # carga 3
    p[6] = random.randint(0, 16000)
    p[7] = random.randint(0, 16000)
    p[8] = random.randint(0, 16000)

    # carga 4
    p[9] = random.randint(0, 12000)
    p[10] = random.randint(0, 12000)
    p[11] = random.randint(0, 12000)

    return p

# funcao de avaliacao das solucoes


# funcao de avaliacao das solucoes
def evaluate_(candidates, args):
    fitness = []
    for cs in candidates:
        fit = perform_fitness(cs)
        fitness.append(fit)
    return fitness


# funcao que calcula o fitness de cada individuo para o problema das
# garrafas
def perform_fitness(p):
    c1_d = float(p[0])  # carga 1 compartimento diânteiro
    c1_c = float(p[1])  # carga 1 compartimento central
    c1_t = float(p[2])  # carga 1 compartimento traseio
    c2_d = float(p[3])  # carga 2 compartimento diânteiro
    c2_c = float(p[4])  # carga 2 compartimento central
    c2_t = float(p[5])  # carga 2 compartimento traseio
    c3_d = float(p[6])  # carga 3 compartimento diânteiro
    c3_c = float(p[7])  # carga 3 compartimento central
    c3_t = float(p[8])  # carga 3 compartimento traseio
    c4_d = float(p[9])  # carga 4 compartimento diânteiro
    c4_c = float(p[10])  # carga 4 compartimento central
    c4_t = float(p[11])  # carga 4 compartimento traseio

    fit = float(0.310*(c1_d+c1_c+c1_t) + 0.380*(c2_d+c2_c+c2_t) +
                0.350*(c3_d+c3_c+c3_t) + 0.285*(c4_d+c4_c+c4_t)) / 12350
    h = [0] * 13

    # restrições peso do compartimento
    h[1] = np.maximum(0, float((c1_d+c2_d+c3_d+c4_d)-10000)
                      ) / 10000  # compartimento 1
    h[2] = np.maximum(0, float((c1_c+c2_c+c3_c+c4_c)-16000)
                      ) / 16000  # compartimento 2
    h[3] = np.maximum(0, float((c1_t+c2_t+c3_t+c4_t)-8000)
                      ) / 8000  # compartimento 3

    # restrição volumétrica
    # volume comp 1
    h[4] = np.maximum(
        0, float((.48*c1_d+.65*c2_d+.58*c3_d+.39*c4_d)-6800))/6800
    # volume comp 2
    h[5] = np.maximum(
        0, float((.48*c1_c+.65*c2_c+.58*c3_c+.39*c4_c)-8700))/8700
    # volume comp 3
    h[6] = np.maximum(
        0, float((.48*c1_t+.65*c2_t+.58*c3_t+.39*c4_t)-5300))/5300

    # restrião peso da carga
    #carga 1
    h[7] = np.maximum(0, float((c1_d+c1_c+c1_t)-18000)
                      ) / 18000
    #carga 2
    h[8] = np.maximum(0, float((c2_d+c2_c+c2_t)-15000)
                      ) / 15000
    #carga 3
    h[9] = np.maximum(0, float((c3_d+c3_c+c3_t)-23000)
                      ) / 23000
    #carga 4
    h[0] = np.maximum(0, float((c4_d+c4_c+c4_t)-12000)
                      ) / 12000

    #restrição de proporção
    h[10] = abs(float((c1_c+ c2_c + c3_c + c4_c)/sum(p))-0.470588)
    h[11] = abs(float((c1_d+ c2_d + c3_d + c4_d)/sum(p))-0.294118)
    h[12] = abs(float((c1_t+ c2_t + c3_t + c4_t)/sum(p))-0.235294)


    fit = float(fit - sum(h))

    return fit

# funcao que faz a avaliacao final do melhor individuo


def solution_evaluation(p):
    c1_d = float(p[0])
    c1_c = p[1]
    c1_t = p[2]  # carga 1 compartimento 3
    c2_d = p[3]
    c2_c = p[4]
    c2_t = p[5]  # carga 2 compartimento 3
    c3_d = p[6]
    c3_c = p[7]
    c3_t = p[8]  # carga 3 compartimento 3
    c4_d = p[9]
    c4_c = p[10]
    c4_t = p[11]  # carga 4 compartimento 3
    # L = cs[0]
    # S = cs[1]
    # L= np.round(L)
    # S = np.round(S)

    print('.. RESUMO DA PRODUCAO:')
    print(f'Lucro total:{float((310*(c1_d+c1_c+c1_t)+ 380*(c2_d+c2_c+c2_t)+ 350*(c3_d+c3_c+c3_t)+ 285*(c4_d+c4_c+c4_t)))}')
    print(f'Carga 1 Compartimento 1: {float(c1_d)}')
    print(f'Carga 1 Compartimento 2: {float(c1_c)}')
    print(f'Carga 1 Compartimento 3: {float(c1_t)}')
    print(f'Carga 2 Compartimento 2: {float(c2_d)}')
    print(f'Carga 2 Compartimento 1: {float(c2_c)}')
    print(f'Carga 2 Compartimento 3: {float(c2_t)}')
    print(f'Carga 3 Compartimento 1: {float(c3_d)}')
    print(f'Carga 3 Compartimento 2: {float(c3_c)}')
    print(f'Carga 3 Compartimento 3: {float(c3_t)}')
    print(f'Carga 4 Compartimento 1: {float(c4_d)}')
    print(f'Carga 4 Compartimento 2: {float(c4_c)}')
    print(f'Carga 4 Compartimento 3: {float(c4_t)}')


def main():
    # funcao principal
    rand = Random()
    rand.seed(int(time()))

    ea = ec.GA(rand)  # instancia da classe de algoritmos geneticos (GA)
    ea.selector = ec.selectors.tournament_selection  # metodo de selecao: torneio
    ea.variator = [ec.variators.uniform_crossover,  # metodo de cruzamento uniforme
                   ec.variators.gaussian_mutation]  # metodo de mutacao
    ea.replacer = ec.replacers.steady_state_replacement

    # funcao que determina o criterio de parada do algoritmo
    # verificar a documentacao para outras opcoes (que sao muitas)
    ea.terminator = terminators.generation_termination
    # funcao para gerar as estatisticas da evolucao
    ea.observer = [ec.observers.stats_observer, ec.observers.file_observer]

    # evolui o AG
    final_pop = ea.evolve(generator=generate_,  # funcao que gera a populacao
                          evaluator=evaluate_,  # funcao que avalia as solucoes
                          pop_size=1000,  # tamanho da populacao
                          maximize=True,  # True: maximizacao, False: minimizacao
                          # limites minimos e maximos dos genes
                          bounder=ec.Bounder(0, 16000),
                          max_generations=10000,  # maximo de geracoes
                          num_inputs=12,  # numero de genes no cromossomo
                          crossover_rate=1.0,  # taxa de cruzamento
                          num_crossover_points=1,  # numero de cortes do cruzamento
                          mutation_rate=0.05,  # taxa de mutacao
                          num_elites=1,  # numero de individuos elites a serem selecionadas para
                          # a proxima populacao
                          num_selected=15,  # numero de individuos
                          tournament_size=2,  # tamanho do torneio
                          # arquivos para estatisticas
                          statistics_file=open("garrafas_stats.csv", 'w'),
                          individuals_file=open("garrafas_individuals.csv", 'w'))

    # Sort and print the best individual, who will be at index 0.
    final_pop.sort(reverse=True)
    print(final_pop[0])

    perform_fitness(final_pop[0].candidate)
    solution_evaluation(final_pop[0].candidate)
    # end_main


main()
