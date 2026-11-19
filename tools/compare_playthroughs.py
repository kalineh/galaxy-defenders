"""Run the same budgeted AI campaigns across difficulties, party sizes and builds."""
import argparse
import csv
import itertools
import subprocess
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def run(case):
    seed, difficulty, players, mode, policy = case
    command = f"debug.run {difficulty} {players} {mode} {policy} {seed}"
    result = subprocess.run([ROOT / "build/headless/galaxy.exe", "--playthrough",
                             "--command", command], check=True, capture_output=True, text=True)
    return list(csv.DictReader(result.stdout.splitlines()))


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--seeds", nargs="+", type=int, default=[42])
    parser.add_argument("--jobs", type=int, default=4)
    parser.add_argument("--out", type=Path, default=ROOT / "artifacts/native-port/playthroughs.csv")
    args = parser.parse_args()
    cases = itertools.product(args.seeds, range(3), (1, 2), ("aim", "dodge"), ("starter", "beam"))
    args.out.parent.mkdir(parents=True, exist_ok=True)
    with ThreadPoolExecutor(max_workers=args.jobs) as workers, args.out.open("w", newline="") as out:
        writer = None
        for rows in workers.map(run, cases):
            if writer is None:
                writer = csv.DictWriter(out, fieldnames=rows[0].keys())
                writer.writeheader()
            writer.writerows(rows)
            out.flush()
    print(args.out)


if __name__ == "__main__":
    main()
