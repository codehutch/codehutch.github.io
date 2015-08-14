Progletariat.com Blog Source
============================

- Started out as a fork of [FsBlog] (https://github.com/fsprojects/FsBlog/)

- The **source** lives in the `source` branch within the `codehutch` repo: https://github.com/codehutch/codehutch.github.io/tree/source 
- The `master` branch is **generated** from the `source` branch: `./fake.sh generate` ...
- ... and then **published** back to the codehutch github master branch: `./fake.sh GitPublish`
- This is viewable as a **preview** at http://preview.progletariat.com

- Once complete, generated changes from the master branch will be pulled into https://github.com/progletariat/progletariat.github.io
- And should then be available at http://www.progletariat.com