import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
import os
import time
import sys
"""
Time,External Nutrition,Internal Nutrition,Active Hyphal,Inactive Hyphal,Tip
"""


def create_plot(x_axis, data, y_label, path, title, plot_type="plot"):
    y_axis = data
    fig, ax = plt.subplots(figsize=(12, 9))
    a = getattr(ax, plot_type)
    marker = "." if plot_type != "plot" else None
    # print(plot_type, marker)
    a(x_axis,
            y_axis,
            marker=marker,
            c='orange',
            linewidth=2)
    ax.set_title(f"{title} Over Time")
    ax.set_ylabel(y_label)
    ax.set_xlabel("Time")
    fig.savefig(os.path.join(path, title.replace(" ", "")))


if __name__ == "__main__":
    data_file = sys.argv[1]
    path = os.path.join(os.curdir, "data")
    path = os.path.join(path, str(time.time()))
    os.mkdir(path)
    data = pd.read_csv(data_file)
    x = data["Time"]
    create_plot(x, data["Tip"], "Tip", path, "Tips Cells", "scatter")
    create_plot(x, data["Active Hyphal"], "Active Hyphal", path, "Active Cells")
    create_plot(x, data["External Nutrition"], "External Nutrition", path, "External Nutrition")
    create_plot(x, data["Internal Nutrition"], "Internal Nutrition", path, "Internal Nutrition")
    create_plot(x, data["Inactive Hyphal"], "Inactive Hyphal", path, "Inactive Cells", "scatter")

    hyphal_sum = data["Inactive Hyphal"] + data["Active Hyphal"] + data["Tip"]
    # print(np.asarray(hyphal_sum))
    create_plot(x, hyphal_sum,
                "Hyphal Cells", path, "Hyphal Cells")
    #
    create_plot(range(len(hyphal_sum) - 1), [hyphal_sum[i+1] - hyphal_sum[i] for i in range(len(hyphal_sum) - 1)],
                "Hyphal Difference", path, "Hyphal Difference", "scatter")

    create_plot(x, hyphal_sum / (data['Tip']-13), "HGU", path, "HGU")
    # fig, ax = plt.subplots(figsize=(12, 9))
    # for i in dir(ax):
    #     print(i)
    #
    # a = getattr(ax, "scatter")(range(10), range(9, -1, -1))
    #
    # fig.show()
    # print(a)
"""Time,External Nutrition,Internal Nutrition,Active Hyphal,Inactive Hyphal,Tip"""


