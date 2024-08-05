FROM node:18-alpine3.19

WORKDIR /app

COPY package.json .

RUN npm install --no-audit

RUN npm i -g serve

COPY . .

RUN npm run build

EXPOSE 3000

CMD [ "serve", "-s", "dist" ]