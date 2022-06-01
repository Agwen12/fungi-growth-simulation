import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
import os
import time
import sys


"""
Time,External Nutrition,Internal Nutrition,Active Hyphal,Tip
"""


def create_plot(x_axis, data, y_label, path, title, plot_type="plot"):
    y_axis = data
    fig, ax = plt.subplots(figsize=(12, 9))
    a = getattr(ax, plot_type)
    marker = "." if plot_type != "plot" else None
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
    hyphal_sum = data["Active Hyphal"] + data["Tip"]

    create_plot(x, data["Tip"], "Tip", path, "Tips Cells", "scatter")
    create_plot(x, data["Active Hyphal"], "Active Hyphal", path, "Active Cells")
    create_plot(x, data["External Nutrition"], "External Nutrition", path, "External Nutrition")
    create_plot(x, data["Internal Nutrition"], "Internal Nutrition", path, "Internal Nutrition")
    create_plot(x, data["Internal Nutrition"]/hyphal_sum, "Average internal nutrition", path, "Average internal nutrition")

    create_plot(x, hyphal_sum,
                "Hyphal Cells", path, "Hyphal Cells")
    create_plot(x[:-1], [hyphal_sum[i+1] - hyphal_sum[i] for i in range(len(hyphal_sum) - 1)],
                "Hyphal Difference", path, "Hyphal Difference", "scatter")

    create_plot(x, hyphal_sum / data['Tip'], "HGU", path, "HGU")
