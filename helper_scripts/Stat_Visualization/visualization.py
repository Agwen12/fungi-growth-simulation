import pandas as pd
import matplotlib.pyplot as plt
import matplotlib
import numpy as np
import os
import time
import sys


"""
Time,External Nutrition,Internal Nutrition,Active Hyphal,Tip
"""


def create_plot(x_axis, data, y_label, title, ax, y_unit="", plot_type="plot"):
    y_axis = data
    # fig, ax = plt.subplots(figsize=(12, 9))
    func = getattr(ax, plot_type)
    marker = "." if plot_type != "plot" else None
    func(x_axis, y_axis, marker=marker, c='orange', linewidth=2)
    ax.set_title(f"{title} Over Time")
    ax.set_ylabel(f"{y_label} {y_unit}")
    ax.set_xlabel("Time (days)")
    # fig.savefig(os.path.join(path, title.replace(" ", "")))


if __name__ == "__main__":
    data_file = sys.argv[1]
    path = os.path.join(os.curdir)
    path = os.path.join(path, str(time.time()))
    os.mkdir(path)
    data = pd.read_csv(data_file)
    x = data["Time"]
    hyphal_sum = data["Active Hyphal"] + data["Tip"]
    plt.rcParams.update({'font.size': 22})
    fig1, ax1 = plt.subplots(figsize=(20, 14), nrows=2, ncols=2, constrained_layout=True)
    fig2, ax2 = plt.subplots(figsize=(20, 14), nrows=2, ncols=2, constrained_layout=True)

    create_plot(x, data["Tip"], "Tip",  "Tips Cells", ax1[0][0])
    create_plot(x, data["Active Hyphal"], "Active Hyphal", "Active Cells", ax1[0][1])
    create_plot(x, hyphal_sum, "Hyphal Cells", "Hyphal Cells", ax1[1][0])
    create_plot(x, hyphal_sum / data['Tip'], "HGU", "HGU", ax1[1][1], "μm")

    create_plot(x, data["External Nutrition"], "External Nutrition", "External Nutrition", ax2[0][0], "mol")
    create_plot(x, data["Internal Nutrition"], "Internal Nutrition", "Internal Nutrition", ax2[0][1], "mol")
    create_plot(x, data["Internal Nutrition"]/hyphal_sum, "Average Internal Nutrition",
                "Average internal nutrition", ax2[1][0], "mol")

    create_plot(x[:-1], [hyphal_sum[i+1] - hyphal_sum[i] for i in range(len(hyphal_sum) - 1)],
                "Hyphal Difference", "Hyphal Difference", ax2[1][1], plot_type="scatter")

    fig1.savefig(os.path.join(path, "fig1"))
    fig2.savefig(os.path.join(path, "fig2"))
