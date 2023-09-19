import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpt_poly_root_abstract_aggr')
export class TptPoly_RootAbstract_Aggr {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  aggrField: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}