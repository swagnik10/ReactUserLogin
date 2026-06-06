interface CardProps {
  children: React.ReactNode;
}

const Card = ({ children }: CardProps) => {
  return (
    <div className="w-full max-w-md rounded-xl bg-white p-6 shadow-lg">
      {children}
    </div>
  );
};

export default Card;