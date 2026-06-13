# Build Stage
FROM node:22-alpine AS build

WORKDIR /app

ARG ENV_FILE=.env

COPY package*.json ./

RUN npm install

COPY . .

RUN cp ${ENV_FILE} .env

RUN npm run build


# Runtime Stage
FROM nginx:alpine

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]