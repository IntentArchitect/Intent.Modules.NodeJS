import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpc_poly_top_level')
export class TpcPoly_TopLevel {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  topField: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}