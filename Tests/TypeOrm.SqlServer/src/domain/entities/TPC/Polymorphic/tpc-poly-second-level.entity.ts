import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpc_poly_second_level')
export class TpcPoly_SecondLevel {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  secondField: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}