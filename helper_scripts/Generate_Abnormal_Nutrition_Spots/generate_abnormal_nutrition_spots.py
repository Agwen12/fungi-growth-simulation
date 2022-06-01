import numpy as np
import functools


n_perc = 0.03
dims = [40, 40, 80] # x, y, z
min_nutri = 1.0
max_nutri = 10.0
mu = [1/20, 1/30, 1/50]
sigma = 0.7
accept_thresh = 0.7


def generate():
    poss_coords = []
    for z in range(dims[2]):
        for y in range(dims[1]):
            for x in range(dims[0]):
                rnds = [np.random.normal(loc=mu[i], scale=sigma) for i in range(3)]
                if (sum(abs(rnds[i]-mu[i]) for i in range(3))) / 3 < accept_thresh:
                    poss_coords.append((x, y, z))


    n = int(n_perc * functools.reduce(lambda x, y: x * y, dims))
    print(f'Generating {n} spots')
    coords_ids = np.random.choice(len(poss_coords), size=n, replace=False, p=None)

    abnormal_nutrition_spots = [(poss_coords[i], 
                                 np.random.uniform(min_nutri, max_nutri)) 
                                for i in coords_ids]

    return abnormal_nutrition_spots


def save_to_file(abnormal_nutrition_spots, 
                 file_path='abnormal_nutrition_spots.txt'):
    print(f'Saving as {file_path}')
    with open(file_path, 'w') as f:
        for ans in abnormal_nutrition_spots:
            f.write(f'({ans[0][0]} {ans[0][1]} {ans[0][2]}) {ans[1]}\n')


save_to_file(generate())
